using System.Collections.Generic;
using System.Linq;
using Repository;

namespace Business
{
    public interface IClassManager
    {
        IEnumerable<ClassModel> Classes { get; }
        ClassModel Class(int classId);
        IEnumerable<ClassModel> UserClassList(int userId);
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

        public ClassModel(int id, string name, string description, decimal price)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }

        public ClassModel() : this(0, "", "", 0.0M)
        {
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
                                    //.Select(a => new ClassModel
                                    //{
                                    //    Id = a.Id,
                                    //    Name = a.Name,
                                    //    Description = a.Description,
                                    //    Price = a.Price
                                    //});
                .Select(a => new ClassModel(a.Id, a.Name, a.Description, a.Price));
            }
        }


        public ClassModel Class(int classId)
        {
            var model = classRepository.Class(classId);
            //return classRepository.Classes
            //                       .Where(a => a.Id == classId)
            //                       .Select(a => new ClassModel
            //                       {
            //                           Id = a.Id,
            //                           Name = a.Name,
            //                           Description = a.Description,
            //                           Price = a.Price
            //                       }).First();
            return new ClassModel(model.Id, model.Name, model.Description, model.Price);
            //return  new ClassModel
            //            {
            //                model.Id, model.Name, model.Description, model.Price
            //            };
        }


        public IEnumerable<ClassModel> UserClassList(int userId)
        {
            var enrolledClasses = classRepository.ForUser(userId)
                                                .Select(a =>
                                                new ClassModel(a.Id, a.Name, a.Description, a.Price));
            //.Select(a => new ClassModel
            //{ 
            //    Id = a.Id,
            //    Name = a.Name,
            //    Description = a.Description,
            //    Price = a.Price
            //});
            return enrolledClasses;
        }
    }
}
