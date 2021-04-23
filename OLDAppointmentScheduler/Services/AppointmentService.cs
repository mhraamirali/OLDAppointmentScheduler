using AppointmentScheduling.Models;
using AppointmentScheduling.Models.ViewModels;
using OLDAppointmentScheduler.Models;
using OLDAppointmentScheduler.Models.ViewModels;
using OLDAppointmentScheduler.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduling.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext applicationDb;
        public AppointmentService(ApplicationDbContext applicationDbContext)
        {
            applicationDb = applicationDbContext;
        }

        public async Task<int> AddUpdate(AppointmentVM model)
        {
            var startDate = DateTime.Parse(model.StartDate);
            var endDate = DateTime.Parse(model.StartDate).AddMinutes(Convert.ToDouble(model.Duration));
            if (model != null && model.Id > 0)
            {
                return 1;
            }
            else
            {
                Appointment appointment = new Appointment()
                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Duration = model.Duration,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId,
                    IsDoctorApproved = false,
                    AdminId = model.AdminId
                };
                applicationDb.Appointments.Add(appointment);
                await applicationDb.SaveChangesAsync();
                return 2;
            }
        }

        public Task<int> ConfirmEvent(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<AppointmentVM> DoctorsEventsById(string doctorId)
        {
            throw new NotImplementedException();
        }

        public AppointmentVM GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<DoctorVM> GetDoctorList()
        {
            var doctors = (from user in applicationDb.Users
                           join userRoles in applicationDb.UserRoles on user.Id equals userRoles.UserId
                           join roles in applicationDb.Roles.Where(x => x.Name == Helper.Doctor) on userRoles.RoleId equals roles.Id
                           select new DoctorVM
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                           ).ToList();

            return doctors;
        }

        public List<PatientVM> GetPatientList()
        {
            var patients = (from user in applicationDb.Users
                            join userRoles in applicationDb.UserRoles on user.Id equals userRoles.UserId
                            join roles in applicationDb.Roles.Where(x => x.Name == Helper.Patient) on userRoles.RoleId equals roles.Id
                            select new PatientVM
                            {
                                Id = user.Id,
                                Name = user.Name
                            }
                ).ToList();

            return patients;
        }

        public List<AppointmentVM> PatientsEventsById(string patientId)
        {
            throw new NotImplementedException();
        }
    }
}
