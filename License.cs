using System;

namespace license_viewer
{
    public class License 
    
    {
        string pinCode {get;set;}
        string registrationNumber {get;set;}
        string path {get;set;}
        string fileName {get;set;}
        //информация о пользователе      
        string userName {get;set;}
        string userSurname {get;set;}    
        string email {get;set;}
        string companyName {get;set;}
        string country {get;set;}
        string potcode {get;set;}
        string region {get;set;}
        string city {get;set;}
        string street {get;set;}
        string houseNumber {get;set;}
        //Информация о продукте
        string description {get;set;}
        string builddate {get;set;}
        string licenseType {get;set;}
        string licenseStatus {get;set;}

        public License(string pinCode, string registrationNumber)
        {
            this.pinCode = pinCode;
            this.registrationNumber = registrationNumber;
            // this.path = path;
            // this.fileName = fileName;
         }

        public void ParseInfo(string info)
        {



        }

    }





}    