using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfExeTracker.Models
{
    //database 
    class EmployeeService
    {
        private static ObservableCollection<Employee> ObjEmployeesList;

        public EmployeeService()
        {
            ObjEmployeesList = new ObservableCollection<Employee>()
            { 
                new Employee { Id = 101, Name = "Peter", Age = 25}
            };
        }

        public ObservableCollection<Employee> GetAll() { return ObjEmployeesList; }

        public bool Add(Employee objNewEmployee)
        {
            // can put validation here

            ObjEmployeesList.Add(new Employee() { 
                Id = objNewEmployee.Id,
                Name = objNewEmployee.Name,
                Age = objNewEmployee.Age
            });
            return true;
        }

        public bool Update(Employee objEmployeeToUpdate)
        {
            bool IsUpdated = false;

            for (int i = 0; i < ObjEmployeesList.Count; i++)
                if (ObjEmployeesList[i].Id == objEmployeeToUpdate.Id)
                {
                    ObjEmployeesList[i].Name = objEmployeeToUpdate.Name;
                    ObjEmployeesList[i].Age = objEmployeeToUpdate.Age;
                    IsUpdated = true;
                    break;
                }
            return IsUpdated;
        }

        public bool Delete(int id)
        {
            bool IsDeleted = false;

            for (int i = 0; i < ObjEmployeesList.Count; i++)
                if (ObjEmployeesList[i].Id == id) 
                { 
                    ObjEmployeesList.RemoveAt(i);
                    IsDeleted = true;
                    break;
                }
            return IsDeleted;
        }

        public Employee Search(int id)
        { 
            return ObjEmployeesList.FirstOrDefault(e => e.Id == id);
        }
    }
}
