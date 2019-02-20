using MyBlog.Data;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{

    public interface IPostService
    {
        void Insert(Post entity);
        void Update(Post entity);
        void Delete(Post entity);
        void Delete(Guid id);
        Post Find(Guid id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetAllByName(string title);
    }

    public class PostService : IPostService
    {

        private readonly IRepository<Post> postRepository;
        private readonly IUnitOfWork unitOfWork;

        public PostService(IRepository<Post> postRepository,IUnitOfWork unitOfWork)
        {
            this.postRepository = postRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Delete(Post entity)
        {
            postRepository.Delete(entity);
            unitOfWork.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var post = postRepository.Find(id);
            if (post != null)
            {
                this.Delete(post);
            }
        }

        public Post Find(Guid id)
        {
            return postRepository.Find(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return postRepository.GetAll();
        }

        public IEnumerable<Post> GetAllByName(string title)
        {
            return postRepository.GetAll(e => e.Title.Contains(title));
        }

        public void Insert(Post entity)
        {
            postRepository.Insert(entity);
            unitOfWork.SaveChanges();
        }

        public void Update(Post entity)
        {
            var post = postRepository.Find(entity.Id);
            post.Title = entity.Title;
            post.Description = entity.Description;
            post.CategoryId = entity.CategoryId;
            postRepository.Update(post);
            unitOfWork.SaveChanges();
        }
    }
}
