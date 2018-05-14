using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IClassRepository
    {
        IEnumerable<ClassModel> Classes { get; }
        ClassModel Class(int classId);
        ClassModel[] ForUser(int userId);
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

        //public ClassModel(int id, string name, string description, decimal price)
        //{
        //    Id = id;
        //    Name = name;
        //    Description = description;
        //    Price = price;
        //}
    }


    public class ClassRepository : IClassRepository
    {
        public ClassModel[] ForUser(int userId)
        {
            return DatabaseAccessor.Instance
                    .Users.First(u => u.UserId == userId)
                    .Classes.Select(a => new ClassModel
                    {
                        Id = a.ClassId,
                        Name = a.ClassName,
                        Description = a.ClassDescription,
                        Price = a.ClassPrice
                    }).ToArray();
        }

        public IEnumerable<ClassModel> Classes
        {
            get
            {
                return DatabaseAccessor.Instance.Classes
                                       .Select(a => new ClassModel
                                       {
                                           Id = a.ClassId,
                                           Name = a.ClassName,
                                           Description = a.ClassDescription,
                                           Price = a.ClassPrice
                                       });
            }
        }


        public ClassModel Class(int classId)
        {
            var class_offered = DatabaseAccessor.Instance.Classes
                                                .Where(a => a.ClassId == classId)
                                                .Select(a => new ClassModel
                                                {
                                                    Id = a.ClassId,
                                                    Name = a.ClassName,
                                                    Description = a.ClassDescription,
                                                    Price = a.ClassPrice
                                                }).First();
            return class_offered;
        }


        //public IEnumerable<ClassModel> ClassList()
        //{
        //    var classes_offered = Classes.Select(a => new ClassModel
        //                            {
        //                                Id = a.Id,
        //                                Name = a.Name,
        //                                Description = a.Description,
        //                                Price = a.Price
        //                            });
        //    return classes_offered;
        //}
    }
}
