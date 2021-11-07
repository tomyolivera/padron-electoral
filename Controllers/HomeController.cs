using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using padron_electoral.Models;

namespace padron_electoral.Controllers
{
    public class HomeController : Controller
    {
        private string ERR_DNI_NOT_FOUND = "El DNI ingresado no se encuentra en el sistema";
        private string ERR_NTRAMITE_NOT_VALID = "El número de trámite no es correcto";
        private string ERR_VOTO_YA_REGISTRADO = "El voto ya fue registrado previamente";
        private string ERR_SISTEMA = "No se pudo completar la acción";
        private string SUCCESS_VOTO = "Voto realizado correctamente";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ConsultarPadron(int DNI)
        {
            Persona persona = BD.ConsultarPadron(DNI);

            if(persona == null)
            {
                ViewBag.ErrorMessage = ERR_DNI_NOT_FOUND;
                return View("Index");
            }

            ViewBag.ErrorMessage = null;
            ViewBag.Persona = persona;
            ViewBag.Establecimiento = BD.ConsultarEstablecimiento(persona.IdEstablecimiento);

            return View("Votar");
        }

        [HttpPost]
        public IActionResult Votar(int DNI, int NumeroTramite)
        {
            Persona persona = BD.ConsultarPadron(DNI);
            ViewBag.ErrorMessage = null;
            ViewBag.SuccessMessage = null;

            if(persona == null)
            {
                ViewBag.ErrorMessage = ERR_DNI_NOT_FOUND;
                return View("Index");
            }

            ViewBag.Persona = persona;
            ViewBag.Establecimiento = BD.ConsultarEstablecimiento(persona.IdEstablecimiento);

            if(persona.Voto)
            {
                ViewBag.ErrorMessage = ERR_VOTO_YA_REGISTRADO;
                return View();
            }

            if(persona.NumeroTramite != NumeroTramite)
            {
                ViewBag.ErrorMessage = ERR_NTRAMITE_NOT_VALID;
                return View();
            }

            if(!BD.Votar(DNI, NumeroTramite))
            {
                ViewBag.ErrorMessage = ERR_SISTEMA;
                return View();
            }

            ViewBag.SuccessMessage = SUCCESS_VOTO;
            return ConsultarPadron(DNI);
        }

        public IActionResult Reporte()
        {
            ViewBag.Reporte = BD.ObtenerReporte();
            return View();
        }
    }
}
