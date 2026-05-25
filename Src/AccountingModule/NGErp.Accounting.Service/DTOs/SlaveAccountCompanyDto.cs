namespace NGErp.Accounting.Service.DTOs;

public record SlaveAccountCompanyDto(
    Guid Id,
    MasterDto Master,
    SlaveDto Slave,
    DetailedAccountDto DetailedAccount1,
    DetailedAccountDto DetailedAccount2
);

public record MasterDto(
    Guid Id,
    int Code,
    string Title
);

public record SlaveDto(
    Guid Id,
    int Code,
    string Title,
    bool LastLevel
);

public record DetailedAccountDto(
    Guid Id,
    int Code,
    string Title,
    bool LastLevel
);