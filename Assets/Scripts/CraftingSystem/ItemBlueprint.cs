public class ItemBlueprint {
    public string itemName;
    public string req1;
    public string req2;
    public int req1Amount;
    public int req2Amount;
    public int numOfRequirements;

    public ItemBlueprint(string name, int reqNum, string R1, int R1num, string R2, int R2num) {
        itemName = name;
        numOfRequirements = reqNum;
        req1 = R1;
        req2 = R2;
        req1Amount = R1num;
        req2Amount = R2num;
    }
}
