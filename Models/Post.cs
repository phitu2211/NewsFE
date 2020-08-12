using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsFE.Models
{
    public class Post
    {
        public Guid id { get; set; }
        public String content { get; set; }
        public String title { get; set; }
        public List<Category> categories { get; set; }
        public String urlImage { get; set; }
    }

    public class Category
    {
        public Guid id { get; set; }
        public List<Category> subCategories { get; set; }
        public String name { get; set; }
        public Guid? parentId { get; set; }
    }

    public class Response<T>
    {
        public T data { get; set; }
    }

    public class PostDetails
    {
        public Post post { get; set; }
        public List<Post> relatedPost{ get; set; }
    }

    public class PostOfCategory
    {
        public List<Post> newsBelongCategories { get; set; }
        public String category { get; set; }
    }
}
