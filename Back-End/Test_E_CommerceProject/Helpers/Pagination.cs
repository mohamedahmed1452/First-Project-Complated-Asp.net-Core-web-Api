using Test_E_CommerceProject.Service.Dtos;

namespace Test_E_CommerceProject.Service.Helpers
{
    public class Pagination<T>
    {
        public int PageIndex { get; set; } //describe the current page
        public int PageSize { get; set; }//describe the number of items in a page
        public int Count { get; set; }//describe the total number of items
        public IReadOnlyList<T> Data { get; }//describe the items in the current page


        public Pagination(int pageIndex, int pageSize, int count,IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
            Count= count;
        }


    }
}
