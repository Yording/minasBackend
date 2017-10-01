using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppMinas.Listas
{
    public class Form
    {
        public string GUID { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }

    }

    public class User
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

    public class Location
    {

        public string Name { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TagUID { get; set; }
        public string LocationTypeGUID { get; set; }
        public string GUID { get; set; }
        public string UpdatedOn { get; set; }
    }



    public class TypeLocation
    {
        public string GUID { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

    }

    public class Activity
    {
        public string Title { get; set; }
        public string GUID { get; set; }
        public string ID { get; set; }
        public string LocationName { get; set; }
        public string Consecutive { get; set; }
        public string AssetName { get; set; }
        public string ParentGUID { get; set; }
    }

    // Estos serian los valores de cada uno de los registros
    // de los formularios
    public class FormValues
    {
        // Este atributo hace refencia a un campo del formulario
        public string apiId { get; set; }
        // Es el valor de ese campo en el formulario
        public object Value { get; set; }
    }

    // Modelo que simboliza cada registro realizado en los formularios
    public class DetailActivity
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string ExternalID { get; set; }
        public string Consecutive { get; set; }
        public string LocationName { get; set; }
        public string AssetName { get; set; }
        public string CompanyStatusName { get; set; }
        public string FormGUID { get; set; }
        public string LocationGUID { get; set; }
        public string AssetGUID { get; set; }
        public string ParentGUID { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public string DueDate { get; set; }
        public string DueTime { get; set; }
        public string DurationMins { get; set; }
        public string CompletedOn { get; set; }
        public string UserName { get; set; }
        public List<FormValues> Values { get; set; }
    }

    public class FormCollection
    {
        public List<Form> Value { get; set; }
    }

    // Modelo que mapea los datos que trae la api de authentication
    public class Token
    {
        public string status { get; set; }
        public string message { get; set; }
        public string AccessToken { get; set; }
    }
}