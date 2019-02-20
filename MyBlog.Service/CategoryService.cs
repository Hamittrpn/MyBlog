using MyBlog.Data;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public interface ICategoryService
    {
        void Insert(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
        void Delete(Guid id);
        Category Find(Guid id);
        IEnumerable<Category> GetAll();
        IEnumerable<Category> GetAllByName(string name);
    }

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categoryRepository;
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IRepository<Category> categoryRepository, IUnitOfWork unitOfWork)
        {
            this.categoryRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Delete(Category entity)
        {
            categoryRepository.Delete(entity);
            unitOfWork.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var category = categoryRepository.Find(id);
            if (category != null)
            {
                this.Delete(category);
            }
        }

        public Category Find(Guid id)
        {
            return categoryRepository.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return categoryRepository.GetAll();
        }

        public IEnumerable<Category> GetAllByName(string name)
        {
            return categoryRepository.GetAll(n => n.Name.Contains(name));
        }

        public void Insert(Category entity)
        {
            categoryRepository.Insert(entity);
            unitOfWork.SaveChanges();
        }

        public void Update(Category entity)
        {
            var category = categoryRepository.Find(entity.Id);
            category.Name = entity.Name;
            category.Description = entity.Description;
            categoryRepository.Update(category);
            unitOfWork.SaveChanges();
        }
    }
}
