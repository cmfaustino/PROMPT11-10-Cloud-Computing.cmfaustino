using System;
using Microsoft.WindowsAzure.StorageClient;

namespace TableUtilities
{
    public class Contact : TableServiceEntity
    {
        public Contact() : base()
        {
        }

        public Contact(string usa, string pallmann) : base(usa, pallmann)
        {
            //throw new NotImplementedException();
            this.Timestamp = DateTime.Now;
        }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }
    }
}