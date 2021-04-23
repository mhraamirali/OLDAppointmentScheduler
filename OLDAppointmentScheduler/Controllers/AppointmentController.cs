using AppointmentScheduling.Services;
using Microsoft.AspNetCore.Mvc;
using OLDAppointmentScheduler.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OLDAppointmentScheduler.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService appointmentService;
        public AppointmentController(IAppointmentService appointment)
        {
            appointmentService = appointment;
        }
        public IActionResult Index()
        {
            ViewBag.Duration = Helper.GetTimeDropDown();
            ViewBag.DoctorList= appointmentService.GetDoctorList();
            ViewBag.PatientList = appointmentService.GetPatientList();
            return View();
        }
    }
}
