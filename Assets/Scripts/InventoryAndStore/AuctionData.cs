namespace InventoryAndStore {
    public struct AuctionData {
        public string Item;
        public int SellValue;
        public int UserId;
        public AuctionStateEnum AuctionState;
        public enum AuctionStateEnum { Sold, ForSale, Return }
    }
}
