using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public class UserService
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;

        // GET all users
        public List<User> GetAll() => _users;

        // GET by id
        public User? GetById(int id) =>
            _users.FirstOrDefault(u => u.Id == id);

        // CREATE
        public User Create(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
            return user;
        }

        // UPDATE
        public bool Update(int id, User updatedUser)
        {
            var user = GetById(id);
            if (user == null) return false;

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Department = updatedUser.Department;

            return true;
        }

        // DELETE
        public bool Delete(int id)
        {
            var user = GetById(id);
            if (user == null) return false;

            _users.Remove(user);
            return true;
        }
    }
}