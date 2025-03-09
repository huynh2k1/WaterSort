public interface IBottle
{
    bool IsFull(); //Lọ đầy hay không
    bool IsEmpty(); //Lọ rỗng hay không
    TypeWater GetTopType(); //Lấy kiểu trên đầu
    int CountTopSameType(); //Lấy tổng số nước cùng màu đầu tiên

    int GetIDTopWater(); //Lấy index của phần tử nước trên cùng

    int GetIDTopEmpty(); //Lấy index của phần tử rỗng ở trên cùng

}

