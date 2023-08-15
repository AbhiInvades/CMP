using CMP_Server_API.CMP.Data.Repositories;
using CMP_Server_API.CMP.Data.Repositories.ClinicRepository;
using CMP_Server_API.Controllers.Clinic_Management;
using CMP_Server_API.Models.Clinic_Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CMP_Server_API.Tests
{
    public class ClinicTest
    {
        private readonly ClinicController _clinicController;
        private readonly IClinicRepository _repository;
        public ClinicTest()
        {
            //Arragnge
            
            _repository = A.Fake<IClinicRepository>();
            
            _clinicController = new ClinicController(_repository);

            //Act
           
        }

        [Fact]
        public void ClinicController_Index_TaskIActionResult()
        {
            var fakeClinics = A.CollectionOfDummy<Clinic>(5).ToList();
            A.CallTo(() => _repository.GetAll()).Returns(Task.FromResult(fakeClinics));
            IEnumerable<Clinic> clinics = _clinicController.Index() as IEnumerable<Clinic>;
            Assert.NotNull(clinics);
        }

        //[Fact]
        //public void ClinicController_Details_TaskIActionResult()
        //{
        //    var fakeClinics = A.Dummy<Clinic>();
        //    A.CallTo(() => _repository.Get(id: 3)).Returns(Task.FromResult(fakeClinics));
        //    IEnumerable<Clinic> clinics = _clinicController.Details(3) as IEnumerable<Clinic>;
        //    Assert.NotNull(clinics);
        //}
    }
}
