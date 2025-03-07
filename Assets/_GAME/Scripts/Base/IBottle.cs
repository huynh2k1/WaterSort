public interface IBottle
{
    bool IsFull(); //Lọ đầy hay không
    bool IsEmpty(); //Lọ rỗng hay không
    TypeWater GetTopType(); //Lấy kiểu trên đầu
    int GetTopLayerAmount(); //Lấy tổng số nước cùng màu đầu tiên
}

