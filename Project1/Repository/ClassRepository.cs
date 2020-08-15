using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Repository
{
    public interface IClassRepository
    {
        //get all available classes
        ClassModel[] Classes { get; }

        //get classes of a specific user
        ClassModel[] myClasses(int userId);

        //get 1 specific class
        ClassModel Class(int classId);

        //add a class to myClass
        //returns the new list of the user's class
        ClassModel[] addClass(int classId, int userId);
    }

    public class ClassModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class ClassRepository : IClassRepository
    {
        public ClassModel[] myClasses(int userId)
        {
            return DatabaseAccessor.Instance.Users.First(
                t => t.UserId == userId).Classes.Select(
                t => new ClassModel
                {
                    Id = t.ClassId,
                    Name = t.ClassName,
                    Description = t.ClassDescription,
                    Price = t.ClassPrice
                }).ToArray();
        }

        public ClassModel[] Classes 
        {
            get
            {
                return DatabaseAccessor.Instance.Classes.Select(
                    t => new ClassModel
                    {
                        Id = t.ClassId,
                        Name = t.ClassName,
                        Description = t.ClassDescription,
                        Price = t.ClassPrice
                    }).ToArray();
            }
                
        }

        public ClassModel Class(int classId)
        {
            var Myclass = DatabaseAccessor.Instance.Classes.Where(
                t => t.ClassId == classId).Select(
                t => new ClassModel
                {
                    Id = t.ClassId,
                    Name = t.ClassName,
                    Description = t.ClassDescription,
                    Price = t.ClassPrice
                }).First();

            return Myclass;
        }

        public ClassModel[] addClass(int classId, int userId)
        {
            var newClass = DatabaseAccessor.Instance.Classes.First(t => t.ClassId == classId);
            var theUser = DatabaseAccessor.Instance.Users.First(t => t.UserId == userId);

            theUser.Classes.Add(newClass);

            DatabaseAccessor.Instance.SaveChanges();

            return myClasses(userId);
        }
    }
}
