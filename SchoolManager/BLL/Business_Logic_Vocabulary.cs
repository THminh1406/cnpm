using SchoolManager.DAL;
using SchoolManager.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace SchoolManager.BLL
{
    public class Business_Logic_Vocabulary
    {
        private data_Access_Vocabulary dal_Vocab = new data_Access_Vocabulary();
        private data_Access_Categories dal_Categories = new data_Access_Categories();

        public bool SaveVocabulary(VocabularyDTO vocab)
        {
            // 1. Kiểm tra chung
            if (string.IsNullOrWhiteSpace(vocab.WordText))
            {
                throw new Exception("Nội dung (Từ vựng, Câu, Điền từ) không được để trống.");
            }
            if (vocab.id_category == 0)
            {
                throw new Exception("Bạn phải chọn một Chủ đề.");
            }

            // 2. Kiểm tra theo loại
            switch (vocab.VocabType)
            {
                case "Word":
                    // Chỉ kiểm tra ảnh khi Thêm Mới
                    if (vocab.WordImage == null && vocab.id_vocabulary == 0)
                    {
                        throw new Exception("Loại 'Từ vựng' (Word) bắt buộc phải có hình ảnh.");
                    }
                    break;

                case "Sentence":
                    // Không cần kiểm tra gì thêm
                    break;

                case "FillBlank":
                    // === LOGIC MỚI ===
                    // Kiểm tra xem có dấu [ và ] không
                    if (!vocab.WordText.Contains("[") || !vocab.WordText.Contains("]"))
                    {
                        throw new Exception("Loại 'Điền từ' (FillBlank) bắt buộc phải có [từ] để đánh dấu đáp án.");
                    }
                    break;
            }

            // 3. Nếu mọi thứ OK, gọi DAL
            return dal_Vocab.SaveVocabulary(vocab);
        }

        public DataTable GetVocabulary(int categoryId, string searchTerm)
        {
            return dal_Vocab.GetVocabulary(categoryId, searchTerm);
        }

        public List<VocabularyDTO> GetVocabularyByCategory(int categoryId, string vocabType)
        {
            // Chỉ cần truyền 2 tham số xuống DAL
            return dal_Vocab.GetVocabularyByCategory(categoryId, vocabType);
        }

        public List<VocabularyDTO> GetVocabularyByIds(List<int> ids)
        {
            return dal_Vocab.GetVocabularyByIds(ids);
        }

        public bool DeleteVocabulary(int vocabId)
        {
            if (vocabId <= 0) return false;

            // 1. Lấy ID chủ đề TRƯỚC KHI XÓA
            int categoryId = dal_Vocab.GetCategoryIdForVocab(vocabId);

            // 2. Xóa từ vựng
            bool success = dal_Vocab.DeleteVocabulary(vocabId);

            // 3. Nếu xóa thành công VÀ từ này thuộc 1 chủ đề
            if (success && categoryId > 0)
            {
                // 4. Đếm xem chủ đề đó còn từ nào không
                int remainingCount = dal_Vocab.GetVocabularyCountByCategory(categoryId);
                if (remainingCount == 0)
                {
                    // 5. Nếu không còn, xóa luôn chủ đề
                    dal_Categories.DeleteCategory(categoryId);
                }
            }
            return success;
        }
    }
}