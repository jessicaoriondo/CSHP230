using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEF
{
    class Program
    {
        static void Main()
        {
            // Database accessor. This opens the database automatically
            var school = new SchoolEntities();

            // This accesses the ClassMaster table
            //foreach (var classMaster in school.ClassMasters)
            //{
            //    Console.WriteLine("ClassId: {0}\nClassName: {1}\nClassDescription: {2}\nClassPrice: {3}\n",
            //        classMaster.ClassId, classMaster.ClassName, classMaster.ClassDescription, classMaster.ClassPrice);
            //}

            //This accesses the ClassMaster table
            foreach (var student in school.Users)
            {
                Console.WriteLine(student.UserName);
                foreach (var classes in student.ClassMasters) {

                    Console.WriteLine("ClassId: {0}\nClassName: {1}\n",
                        classes.ClassId, classes.ClassName);
                }

                Console.WriteLine("--");

                //var classIds = school.RetrieveClassesForStudent(student.UserId).ToArray();

                //foreach (var item in classIds)
                //{
                //    var classMaster = school.ClassMasters.FirstOrDefault(t => t.ClassId == item.ClassId);

                //    Console.WriteLine("ClassId: {0} Name: {1}", classMaster.ClassId, classMaster.ClassName);
                //}


            }


            Console.Write("Done.");
            Console.ReadLine();
        }
    }
}
