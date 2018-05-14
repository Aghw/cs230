using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public interface IClassRepository
    {
        IEnumerable<ClassModel> Classes { get; }
        ClassModel Class(int classId);
        IEnumerable<ClassModel> ForUser(int userId);
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
    }


    public class ClassRepository : IClassRepository
    {
        public IEnumerable<ClassModel> ForUser(int userId)
        {
            return DatabaseAccessor.Instance
                    .Users.First(u => u.UserId == userId)
                    .Classes.Select(a => new ClassModel
                    {
                        Id = a.ClassId,
                        Name = a.ClassName,
                        Description = a.ClassDescription,
                        Price = a.ClassPrice
                    });
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
    }
}
