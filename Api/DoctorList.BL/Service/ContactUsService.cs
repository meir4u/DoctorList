using DoctorList.BL.DTOs;
using DoctorList.BL.Interface;
using DoctorList.DL.Entities;
using DoctorList.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorList.BL.Service
{
    public class ContactUsService : IContactUsService
    {
        private readonly IContactUsRepository _contactUsRepository;

        public ContactUsService(IContactUsRepository contactUsRepository)
        {
            this._contactUsRepository = contactUsRepository;
        }

        public bool SaveContactUsData(ContactUsDto contactUsDto)
        {
            ContactUs mapped = mapToEntity(contactUsDto);
            var result = _contactUsRepository.SaveContactUsData(mapped);
            return result;
        }

        private ContactUs mapToEntity(ContactUsDto contactUsDto)
        {
            var mapped = new ContactUs()
            {
                Email = contactUsDto.Email,
                FullName = contactUsDto.FullName,
                PhoneNumber = contactUsDto.PhoneNumber,
            };
            return mapped;
        }
    }
}
