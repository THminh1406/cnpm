using SchoolManager.DAL;
using SchoolManager.DTO;
using System;
using System.Collections.Generic;

namespace SchoolManager.BLL
{
    public class Business_Logic_Categories
    {
        private data_Access_Categories dal_Categories = new data_Access_Categories();

        // (Trong file Business_Logic_Categories.cs)

        public List<CategoryDTO> GetAllCategories(bool onlyShowNonEmpty = false)
        {
            return dal_Categories.GetAllCategories(onlyShowNonEmpty);
        }

        /// <summary>
        /// Lưu chủ đề mới, trả về ID mới
        /// </summary>
        public int SaveCategory(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                throw new Exception("Tên chủ đề không được để trống.");
            }
            // (Bạn có thể thêm logic kiểm tra trùng tên ở đây)

            return dal_Categories.SaveCategory(categoryName);
        }
    }
}