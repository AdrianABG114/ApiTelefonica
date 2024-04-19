using ApiTelefonica.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiTelefonica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactoController : ControllerBase
    {

        [HttpPost]
        public IActionResult AgregarContacto([FromBody] ModelContacto contacto)
        {
            try
            {
                // Verificar que los atributos básicos estén presentes
                if (contacto == null)
                {
                    return BadRequest("El objeto de contacto es nulo.");
                }

                if (string.IsNullOrWhiteSpace(contacto.Nombres) || string.IsNullOrWhiteSpace(contacto.Apellidos))
                {
                    return BadRequest("Los nombres y apellidos del contacto son obligatorios.");
                }

                ContactoRepository.AddContact(contacto);

                return Ok("Contacto agregado correctamente.");
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante el proceso
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult ObtenerTodosLosContactos()
        {
            try
            {
                List<ModelContacto> contactos = ContactoRepository.GetAllContacts();
                return Ok(contactos);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante el proceso
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{nombre}")]
        public IActionResult EliminarContactoPorNombre(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    return BadRequest("El nombre del contacto es obligatorio para eliminarlo.");
                }

                ModelContacto contactoAEliminar = ContactoRepository.GetAllContacts().FirstOrDefault(c => c.Nombres == nombre);
                if (contactoAEliminar == null)
                {
                    return NotFound(); // Si no se encuentra el contacto, devolvemos un código 404 (Not Found)
                }

                ContactoRepository.DeleteContact(contactoAEliminar);
                return Ok("Contacto eliminado correctamente.");
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante el proceso
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{nombre}")]
        public IActionResult ActualizarContactoPorNombre(string nombre, [FromBody] ModelContacto contactoActualizado)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    return BadRequest("El nombre del contacto es obligatorio para actualizarlo.");
                }

                if (contactoActualizado == null)
                {
                    return BadRequest("El objeto de contacto actualizado es nulo.");
                }

                ModelContacto contactoExistente = ContactoRepository.GetAllContacts().FirstOrDefault(c => c.Nombres == nombre);
                if (contactoExistente == null)
                {
                    return NotFound(); // Si no se encuentra el contacto, devolvemos un código 404 (Not Found)
                }

                // Actualizar los campos del contacto existente con los valores del contacto actualizado
                contactoExistente.Nombres = contactoActualizado.Nombres;
                contactoExistente.Apellidos = contactoActualizado.Apellidos;
                contactoExistente.Cumpleano = contactoActualizado.Cumpleano;
                contactoExistente.Numeros = contactoActualizado.Numeros;
                contactoExistente.Emails = contactoActualizado.Emails;

                // Aquí podrías realizar otras operaciones de actualización, como guardar en la base de datos

                return Ok("Contacto actualizado correctamente.");
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante el proceso
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }

}
