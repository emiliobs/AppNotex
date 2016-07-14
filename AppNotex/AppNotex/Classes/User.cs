using SQLite.Net.Attributes;

namespace AppNotex.Classes
{
    public class User
    {
        [PrimaryKey]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{ this.FirstName } { this.LastName }";
            }
                
        }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public bool IsStudent { get; set; }
        public bool IsTeacher { get; set; }

        public string Password { get; set; }

        public string PhotoFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.Photo))
                {
                    return string.Empty;
                }

                return $"http://www.zulu-software.com/notes{this.Photo.Substring(1)}";
            }
         }

        public override int GetHashCode()
        {
            return this.UserId;
        }
    }
}
