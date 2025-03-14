using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Application.Services;
using UserManagementApi.Domain.Models;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserApplicationService _userService;

        public UserController(UserApplicationService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return users;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);

                if (user == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                return user;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User newUser)
        {
            try
            {
                // Asegurarse de que el Id sea nulo para que MongoDB lo genere
                newUser.Id = null;
                
                await _userService.CreateUserAsync(newUser);
                
                // El Id debería haber sido asignado por MongoDB después de la creación
                if (string.IsNullOrEmpty(newUser.Id))
                {
                    return StatusCode(500, "Error al generar ID para el nuevo usuario");
                }
                
                return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] User updatedUser)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);

                if (user == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                updatedUser.Id = id;

                await _userService.UpdateUserAsync(id, updatedUser);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);

                if (user == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                await _userService.DeleteUserAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}