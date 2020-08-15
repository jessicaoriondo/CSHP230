using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Website.Business
{
    public interface IClassManager
    {
        ClassModel[] Classes { get; }
        ClassModel[] myClasses(int userId);
        ClassModel Class(int classId);
        ClassModel[] addClass(int classId, int userId);

    }

    public class ClassModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public ClassModel(int id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
    }

    public class ClassManager : IClassManager
    {
        private readonly IClassRepository classRepository;

        public ClassManager(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }
        public ClassModel[] Classes
        {
            get
            {
                return classRepository.Classes.Select(t => new ClassModel(
                    t.Id, t.Name, t.Description, t.Price)).ToArray();
            }
        }

        public ClassModel Class(int classId)
        {
            var classModel = classRepository.Class(classId);
            return new ClassModel(classModel.Id, classModel.Name, classModel.Description, classModel.Price);
        }

        public ClassModel[] myClasses(int userId)
        {
            return classRepository.myClasses(userId).Select(t => new ClassModel(
                t.Id, t.Name, t.Description, t.Price)).ToArray();
        }

        public ClassModel[] addClass(int classId, int userId)
        {
            return classRepository.addClass(classId, userId).Select(t => new ClassModel(
                t.Id, t.Name, t.Description, t.Price)).ToArray(); ;
        }

    }

}
