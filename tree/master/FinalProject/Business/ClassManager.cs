using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;

namespace Business
{
    public interface IClassManager
    {
        IEnumerable<ClassModel> Classes { get; }
        ClassModel Class(int classId);
        ClassModel[] UserClassList(int userId);
    }

    public class ClassModel
    {
        public int Id { get; set; }

        //[Required(ErrorMessage = "Please enter class name")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Please enter class description")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "Please enter class price")]
        public decimal Price { get; set; }

        public ClassModel (int id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
    }

    class ClassManager: IClassManager
    {
        private readonly IClassRepository classRepository;

        public ClassManager(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }

        public IEnumerable<ClassModel> Classes
        {
            get
            {
                return classRepository.Classes
                                       .Select(a => 
                                       new ClassModel(a.Id, a.Name, 
                                                      a.Description, a.Price));
            }
        }


        public ClassModel Class(int classId)
        {
            var classModel = classRepository.Class(classId);
            return new ClassModel(classModel.Id, classModel.Name,
                                  classModel.Description, classModel.Price);
        }


        public ClassModel[] UserClassList(int userId)
        {
            var enrolledClasses = (ClassModel[]) classRepository.ForUser(userId).ToArray();
            var models = enrolledClasses;
            return models;
        }
    }
}
