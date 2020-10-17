using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;

namespace L04
{
    public class Program
    {
        private static CloudTableClient tableClient;
        private static CloudTable studentsTable;

        public static void Main(string[] args)
        {
            Task.Run(async () => { await Initialize(); })
                .GetAwaiter()
                .GetResult();

        }

        static async Task Initialize()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=datc2020tema4;AccountKey=hJFETfkbLtmSvc7TLGWQMEwJrnLjsbeD+6UywaNi6zyhvCt2XXYQeBmG2gCY4DGG/taBVyVSlAIayPgv2LR3XA==;EndpointSuffix=core.windows.net";

            var account = CloudStorageAccount.Parse(storageConnectionString);
            tableClient = account.CreateCloudTableClient();

            studentsTable = tableClient.GetTableReference("studenti");

            await studentsTable.CreateIfNotExistsAsync();

            // await Add_New_Student();
            // await EditStudent();
            // await DeleteStudent();
        }

        private static async Task Add_New_Student()
        {
            var student = new StudentEntity("UPT", "1970101051111");
            student.FirstName = "Ion";
            student.LastName = "Popescu";
            student.Email = "ion-popescu@gmail.com";
            student.Year = 2;
            student.PhoneNumber = "0722334455";
            student.Faculty = "AC";

            var insertOperation = TableOperation.Insert(student);

            await studentsTable.ExecuteAsync(insertOperation);
        }

        private static async Task EditStudent()
        {
            var student = new StudentEntity("UPT", "1970101051111");
            student.FirstName = "Ion";
            student.LastName = "Popescu";
            student.Email = "ion-popescu@gmail.com";
            student.Year = 3;
            student.PhoneNumber = "0722334455";
            student.Faculty = "TR";

            var editOperation = TableOperation.InsertOrReplace(student);

            await studentsTable.ExecuteAsync(editOperation);
        }

        private static async Task DeleteStudent()
        {
            var student = new StudentEntity("UPT", "1970101051111");
            student.FirstName = "Ion";
            student.LastName = "Popescu";
            student.Email = "ion-popescu@gmail.com";
            student.Year = 3;
            student.PhoneNumber = "0722334455";
            student.Faculty = "TR";

            var retrieveOperation = TableOperation.Retrieve<StudentEntity>("UPT", "1970101051111");

            var deleteOperation = TableOperation.Delete(student);

            await studentsTable.ExecuteAsync(deleteOperation);
        }
    }
}
