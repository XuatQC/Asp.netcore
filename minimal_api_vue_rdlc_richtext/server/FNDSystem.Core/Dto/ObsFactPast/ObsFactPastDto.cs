namespace FNDSystem.Core.Dto
{
    public class ObsFactPastDto
    {
        public required int Id { get; set; }
        public required string Num { get; set; }
        public required int Quantity { get; set; }
        public required string SerialNum { get; set; }
        public required string Fact { get; set; }
        public required bool FactTrans { get; set; }
        public required string Judge { get; set; }
        public required string PartTrans { get; set; }
        public required int? PlusNeutralDelta { get; set; }
        public required bool? ValComp { get; set; }
        public required bool? OferField1 { get; set; }
        public required bool? OferField2 { get; set; }
        public required bool? OferField3 { get; set; }
        public required bool? OferField4 { get; set; }
        public required bool? OferField5 { get; set; }
        public required bool? OferField6 { get; set; }
        public required bool? OferField7 { get; set; }
        public required bool? OferField8 { get; set; }
        public required bool? OferField9 { get; set; }
        public required bool? OferField10 { get; set; }
        public required bool? OferField11 { get; set; }
        public required string? Poc1 { get; set; }
        public required string? Poc2 { get; set; }
        public required string? Poc3 { get; set; }
        public required string? Poc4 { get; set; }
        public required string? Poc5 { get; set; }
        public required string? Poc6 { get; set; }
        public required string? Poc7 { get; set; }
        public required string? Poc8 { get; set; }
        public required int? SeqNum { get; set; }
        public required DateTime? LastUpdate { get; set; }
    }
}
