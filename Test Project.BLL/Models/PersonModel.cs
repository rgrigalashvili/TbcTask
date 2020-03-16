using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using Test_Project.Shared.Models;
using Test_Project.Shared.Resources;

namespace Test_Project.BLL.Models
{
    public class PersonModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GenderName { get; set; }
        public string PrivateNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? CityId { get; set; }
        public ICollection<MobileNumberModel> MobileNumbers { get; set; }
        public string ImageName { get; set; }
        public string Image { get; set; }
        public ICollection<RelatedPersonModel> RelatedPersons { get; set; }
        public override OperationOutcome Validate()
        {
            if (string.IsNullOrEmpty(FirstName) || FirstName.Length < 2 || FirstName.Length > 50 || !Regex.IsMatch(FirstName, ("^[\u10A0-\u10FF]+$|^[a-zA-Z]+$")))
            {
                return new OperationOutcome(false, GlobalResource.FirstNameIsIncorrect);
            }
            if (string.IsNullOrEmpty(LastName) || LastName.Length <= 2 || LastName.Length >= 50 || !Regex.IsMatch(LastName, ("^[\u10A0-\u10FF]+$|^[a-zA-Z]+$")))
            {
                return new OperationOutcome(false, GlobalResource.LastNameIsIncorrect);
            }
            if (string.IsNullOrEmpty(GenderName) || !(GenderName.ToLower() == "female" || GenderName.ToLower() == "male"))
            {
                return new OperationOutcome(false, GlobalResource.GenderIsIncorrect);
            }
            else
            {
                GenderName = GenderName.ToLower();
            }
            if (string.IsNullOrEmpty(PrivateNumber) || PrivateNumber.Length != 11 || !Regex.IsMatch(PrivateNumber, ("^[0-9]+$")))
            {
                return new OperationOutcome(false, GlobalResource.PrivateNumberIsIncorrect);
            }
            if (DateOfBirth == null || !(DateOfBirth.Value.AddYears(18) < DateTime.Now))
            {
                return new OperationOutcome(false, GlobalResource.AgeMustBeMoreThan18Years);
            }

            if (MobileNumbers != null && MobileNumbers.Count > 0)
            {
                OperationOutcome validationResut;
                foreach (var item in MobileNumbers)
                {
                    validationResut = item.Validate();
                    if (!validationResut.IsSuccess)
                    {
                        return validationResut;
                    }
                }
            }
            if (RelatedPersons != null && RelatedPersons.Count > 0)
            {
                OperationOutcome validationResut;
                foreach (var item in RelatedPersons)
                {
                    validationResut = item.Validate();
                    if (!validationResut.IsSuccess)
                    {
                        return validationResut;
                    }
                }
            }
            return new OperationOutcome(true, GlobalResource.Success);
        }
    }
}
