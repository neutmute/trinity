namespace Trinity.IntegrityChecks
{
    public enum RelationshipThresholdType
    {
        Unspecified = 0,
        ReturnIfGreaterThan = 1,
        ReturnIfNotExact = 2,
        ReturnIfEqual = 3
    }

    public enum RelationshipDirection
    {
        Unspecified = 0
        ,Outbound
        ,Inbound
    }
}