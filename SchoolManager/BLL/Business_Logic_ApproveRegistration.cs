using System;
using System.Collections.Generic;
using SchoolManager.DAL;
using SchoolManager.DTO;

namespace SchoolManager.BLL
{
    public class Business_Logic_ApproveRegistration
    {
        private data_Access_Account dal = new data_Access_Account();
        private data_Access_Classes dalClasses = new data_Access_Classes();

        public List<Accounts> GetPendingRegistrations()
        {
            return dal.GetPendingRegistrations();
        }

        public bool ApproveTeacher(int teacherId)
        {
            try
            {
                return dal.ApproveTeacher(teacherId);
            }
            catch
            {
                return false;
            }
        }

        public bool RejectTeacher(int teacherId)
        {
            try
            {
                return dal.RejectTeacher(teacherId);
            }
            catch
            {
                return false;
            }
        }

        public List<Accounts> GetAllTeachers()
        {
            return dal.GetAllTeachers();
        }

        public bool LockTeacher(int teacherId) => dal.LockTeacher(teacherId);
        public bool UnlockTeacher(int teacherId) => dal.UnlockTeacher(teacherId);
        public bool DeleteTeacher(int teacherId) => dal.DeleteTeacher(teacherId);

        public List<Classes> GetAllClasses() => dalClasses.GetAllClasses();
        public bool AssignTeacherToClass(int classId, int teacherId) => dalClasses.AssignTeacherToClass(classId, teacherId);
    }
}
