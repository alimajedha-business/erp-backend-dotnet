#!/usr/bin/env python3
"""
Scaffold NGErp HCM entity files.

Usage examples:

  python scaffold_entity.py EmployeeEducation --fields "EmployeeId:Guid,LevelCode:string,FieldCode:string,MajoringCode:string,StartDate:DateTime,EndDate:DateTime?,GPA:decimal?,CenterCode:string,EffectiveDate:DateTime"

  python scaffold_entity.py EmployeeEducation --fields-file employee_education.json

JSON example:
{
  "entity": "EmployeeEducation",
  "schema": "HCM",
  "route": "employee-educations",
  "baseClass": "BaseEntityWithCompany",
  "status": false,
  "fields": [
    { "name": "EmployeeId", "type": "Guid", "required": true, "relation": "Employee" },
    { "name": "LevelCode", "type": "string", "required": true, "maxLength": 50, "minLength": 2 },
    { "name": "StartDate", "type": "DateTime", "required": true },
    { "name": "EndDate", "type": "DateTime?" },
    { "name": "GPA", "type": "decimal?", "precision": [5, 2], "range": [0, 20] },
    { "name": "Email", "type": "string?", "maxLength": 254, "email": true }
  ]
}

Supported field metadata:
  required, maxLength, minLength, precision, range, regex/regularExpression,
  email, phone, url, columnType, defaultValue, defaultValueSql, relation,
  index, uniqueWithCompany, attributes, modelAttributes, dtoAttributes,
  createAttributes, patchAttributes, fluent.
"""

from __future__ import annotations

import argparse
import json
import re
import sys
from dataclasses import dataclass, field
from pathlib import Path
from typing import Any


# -----------------------------
# Models
# -----------------------------

@dataclass
class FieldSpec:
    name: str
    type: str
    required: bool = False
    max_length: int | None = None
    min_length: int | None = None
    precision: tuple[int, int] | None = None
    range: tuple[Any, Any] | None = None
    regular_expression: str | None = None
    email: bool = False
    phone: bool = False
    url: bool = False
    column_type: str | None = None
    default_value: Any = None
    default_value_sql: str | None = None
    relation: str | None = None
    index: bool = False
    unique_with_company: bool = False
    attributes: list[str] = field(default_factory=list)
    dto_attributes: list[str] = field(default_factory=list)
    create_attributes: list[str] = field(default_factory=list)
    patch_attributes: list[str] = field(default_factory=list)
    model_attributes: list[str] = field(default_factory=list)
    fluent: list[str] = field(default_factory=list)

    @property
    def is_nullable(self) -> bool:
        return self.type.endswith("?") or self.type == "string"

    @property
    def clean_type(self) -> str:
        return self.type.rstrip("?")

    @property
    def is_string(self) -> bool:
        return self.clean_type == "string"

    @property
    def is_required_in_create(self) -> bool:
        return self.required or (not self.is_nullable and self.clean_type != "bool")

    @property
    def model_type(self) -> str:
        if self.required and self.clean_type == "string":
            return "string"
        return self.type


@dataclass
class EntitySpec:
    entity: str
    schema: str = "HCM"
    module: str = "HCM"
    base_class: str = "BaseEntityWithCompany"
    route: str | None = None
    status: bool = False
    fields: list[FieldSpec] = field(default_factory=list)

    @property
    def entity_camel(self) -> str:
        return self.entity[:1].lower() + self.entity[1:]

    @property
    def route_name(self) -> str:
        return self.route or to_kebab_plural(self.entity)


# -----------------------------
# Naming helpers
# -----------------------------

def to_kebab_plural(name: str) -> str:
    words = re.findall(r"[A-Z]?[a-z]+|[A-Z]+(?=[A-Z]|$)|\d+", name)
    kebab = "-".join(w.lower() for w in words)
    if kebab.endswith("y"):
        return kebab[:-1] + "ies"
    if kebab.endswith("s"):
        return kebab + "es"
    return kebab + "s"


def singular_from_id(field_name: str) -> str | None:
    if field_name.endswith("Id") and len(field_name) > 2:
        return field_name[:-2]
    return None


def dto_type_for_create(field: FieldSpec) -> str:
    if field.required and field.clean_type == "string":
        return "string"
    if field.clean_type in {"Guid", "DateTime", "DateOnly", "TimeOnly", "decimal", "int", "long", "bool"}:
        return field.clean_type + "?"
    return field.type


def dto_type_for_patch(field: FieldSpec) -> str:
    if field.clean_type == "string":
        return "string?"
    if field.clean_type in {"Guid", "DateTime", "DateOnly", "TimeOnly", "decimal", "int", "long", "bool"}:
        return field.clean_type + "?"
    return field.type


def cs_default_property(field: FieldSpec) -> str:
    if field.is_string:
        return " = default!;" if field.required else ""
    if field.type.endswith("?"):
        return ""
    return ""


def normalize_attr(attr: str) -> str:
    attr = attr.strip()
    if not attr:
        return attr
    return attr if attr.startswith("[") else f"[{attr}]"


def normalize_fluent_call(call: str) -> str:
    call = call.strip().rstrip(";")
    if not call:
        return call
    return call if call.startswith(".") else f".{call}"


def cs_literal(value: Any) -> str:
    if isinstance(value, bool):
        return "true" if value else "false"
    if isinstance(value, (int, float)):
        return str(value)
    if value is None:
        return "null"
    escaped = str(value).replace("\\", "\\\\").replace('"', '\\"')
    return f'"{escaped}"'


def field_data_annotations(field: FieldSpec, include_required: bool) -> list[str]:
    attrs: list[str] = []
    if include_required and field.is_required_in_create:
        attrs.append("[Required]")
    if field.max_length is not None:
        attrs.append(f"[MaxLength({field.max_length})]")
    if field.min_length is not None:
        attrs.append(f"[MinLength({field.min_length})]")
    if field.range is not None:
        attrs.append(f"[Range({cs_literal(field.range[0])}, {cs_literal(field.range[1])})]")
    if field.regular_expression:
        pattern = field.regular_expression.replace("\\", "\\\\").replace('"', '\\"')
        attrs.append(f'[RegularExpression("{pattern}")]')
    if field.email:
        attrs.append("[EmailAddress]")
    if field.phone:
        attrs.append("[Phone]")
    if field.url:
        attrs.append("[Url]")
    attrs.extend(normalize_attr(attr) for attr in field.attributes)
    return [attr for attr in attrs if attr]


def property_with_attributes(
    property_type: str,
    property_name: str,
    suffix: str,
    attrs: list[str],
    indent: str = "    ",
) -> str:
    lines = [f"{indent}{attr}" for attr in attrs]
    lines.append(f"{indent}public {property_type} {property_name} {{ get; set; }}{suffix}")
    return "\n".join(lines)


def list_from_json(value: Any) -> list[str]:
    if value is None:
        return []
    if isinstance(value, str):
        return [value]
    return [str(item) for item in value]


# -----------------------------
# Parsing
# -----------------------------

def parse_fields_inline(fields_raw: str) -> list[FieldSpec]:
    fields: list[FieldSpec] = []
    for part in [p.strip() for p in fields_raw.split(",") if p.strip()]:
        if ":" not in part:
            raise ValueError(f"Invalid field '{part}'. Use Name:Type.")
        name, typ = [x.strip() for x in part.split(":", 1)]
        relation = singular_from_id(name)
        fields.append(FieldSpec(
            name=name,
            type=typ,
            required=not typ.endswith("?") and typ != "string",
            relation=relation,
            index=name.endswith("Id"),
            max_length=50 if typ == "string" else None,
            precision=(5, 2) if typ.rstrip("?") == "decimal" else None,
        ))
    return fields


def parse_tuple(value: Any, field_name: str, metadata_name: str) -> tuple[Any, Any] | None:
    if value is None:
        return None
    if not isinstance(value, (list, tuple)) or len(value) != 2:
        raise ValueError(f"Field '{field_name}' metadata '{metadata_name}' must be a two-item array.")
    return (value[0], value[1])


def parse_field_spec(data: dict[str, Any]) -> FieldSpec:
    name = data["name"]
    typ = data["type"]
    precision = parse_tuple(data.get("precision"), name, "precision")
    parsed_range = parse_tuple(data.get("range"), name, "range")
    return FieldSpec(
        name=name,
        type=typ,
        required=bool(data.get("required", False)),
        max_length=data.get("maxLength", data.get("max_length")),
        min_length=data.get("minLength", data.get("min_length")),
        precision=tuple(int(x) for x in precision) if precision else None,
        range=parsed_range,
        regular_expression=data.get("regularExpression", data.get("regex")),
        email=bool(data.get("email", False)),
        phone=bool(data.get("phone", False)),
        url=bool(data.get("url", False)),
        column_type=data.get("columnType", data.get("column_type")),
        default_value=data.get("defaultValue", None),
        default_value_sql=data.get("defaultValueSql", data.get("default_value_sql")),
        relation=data.get("relation") or singular_from_id(name),
        index=bool(data.get("index", name.endswith("Id"))),
        unique_with_company=bool(data.get("uniqueWithCompany", False)),
        attributes=list_from_json(data.get("attributes")),
        dto_attributes=list_from_json(data.get("dtoAttributes", data.get("dto_attributes"))),
        create_attributes=list_from_json(data.get("createAttributes", data.get("create_attributes"))),
        patch_attributes=list_from_json(data.get("patchAttributes", data.get("patch_attributes"))),
        model_attributes=list_from_json(data.get("modelAttributes", data.get("model_attributes"))),
        fluent=list_from_json(data.get("fluent")),
    )


def load_spec(args: argparse.Namespace) -> EntitySpec:
    if args.fields_file:
        data = json.loads(Path(args.fields_file).read_text(encoding="utf-8"))
        fields = [parse_field_spec(f) for f in data.get("fields", [])]
        return EntitySpec(
            entity=data.get("entity", args.entity),
            schema=data.get("schema", args.schema),
            module=data.get("module", args.module),
            base_class=data.get("baseClass", args.base_class),
            route=data.get("route"),
            status=bool(data.get("status", args.status)),
            fields=fields,
        )

    if not args.fields:
        raise ValueError("Provide --fields or --fields-file.")

    return EntitySpec(
        entity=args.entity,
        schema=args.schema,
        module=args.module,
        base_class=args.base_class,
        route=args.route,
        status=args.status,
        fields=parse_fields_inline(args.fields),
    )


def parse_required_answer(answer: str, field_names: list[str]) -> set[str] | None:
    answer = answer.strip()
    if not answer:
        return None
    lowered = answer.lower()
    if lowered in {"none", "no", "-"}:
        return set()
    if lowered in {"all", "*"}:
        return set(field_names)

    by_lower = {name.lower(): name for name in field_names}
    required: set[str] = set()
    for raw_name in re.split(r"[,;\s]+", answer):
        if not raw_name:
            continue
        key = raw_name.lower()
        if key not in by_lower:
            raise ValueError(f"Unknown required field '{raw_name}'. Known fields: {', '.join(field_names)}")
        required.add(by_lower[key])
    return required


def prompt_required_fields(spec: EntitySpec, enabled: bool) -> None:
    if not enabled or not sys.stdin.isatty():
        return

    field_names = [field.name for field in spec.fields]
    defaults = [field.name for field in spec.fields if field.is_required_in_create]
    default_text = ", ".join(defaults) if defaults else "none"
    print("")
    print(f"Fields: {', '.join(field_names)}")
    print(f"Current required fields: {default_text}")
    print("Enter required fields as comma-separated names, 'all', or 'none'. Press Enter to keep current.")

    while True:
        answer = input("Required fields: ")
        try:
            parsed = parse_required_answer(answer, field_names)
        except ValueError as exc:
            print(exc)
            continue
        if parsed is None:
            return
        for field_spec in spec.fields:
            field_spec.required = field_spec.name in parsed
        return


# -----------------------------
# Project path detection
# -----------------------------

def find_project_root(start: Path) -> Path:
    current = start.resolve()
    for p in [current, *current.parents]:
        if any(p.glob("*.sln")) or (p / "src").exists():
            return p
    return current


def find_module_project(root: Path, module: str, suffix: str) -> Path:
    """Find module project folder.

    Supports both layouts:
      src/HCMModule/NGErp.HCM.Domain
      src/HCMModule/NGErp.HCM.Domain/NGErp.HCM.Domain.csproj

    Also prints nearby candidates when it cannot find the folder, so fixing paths is easy.
    """
    expected = f"NGErp.{module}.{suffix}"

    # Best option: find by .csproj name.
    csproj_matches = list(root.rglob(f"{expected}.csproj"))
    if csproj_matches:
        return csproj_matches[0].parent

    # Fallback: find by directory name.
    dir_matches = [p for p in root.rglob("*") if p.is_dir() and p.name.lower() == expected.lower()]
    if dir_matches:
        return dir_matches[0]

    # Helpful error output.
    nearby = sorted(
        str(p.relative_to(root))
        for p in root.rglob("*.csproj")
        if f".{module}." in p.name or module.lower() in str(p).lower()
    )
    hint = ""
    if nearby:
        hint = "\nFound these related project files:\n" + "\n".join(
            f"  - {x}" for x in nearby
        )

    raise FileNotFoundError(
        f"Could not find project folder/project file {expected} under {root}.{hint}"
    )


@dataclass
class ProjectPaths:
    root: Path
    domain: Path
    service: Path
    infrastructure: Path
    api: Path


def resolve_paths(root: Path, module: str) -> ProjectPaths:
    return ProjectPaths(
        root=root,
        domain=find_module_project(root, module, "Domain"),
        service=find_module_project(root, module, "Service"),
        infrastructure=find_module_project(root, module, "Infrastructure"),
        api=find_module_project(root, module, "API"),
    )


# -----------------------------
# Code generation
# -----------------------------

def generate_entity(spec: EntitySpec) -> str:
    nav_props = []
    relation_configs = []
    index_configs = []
    property_configs = []

    for f in spec.fields:
        if f.index:
            index_configs.append(f'''        builder
            .HasIndex(e => e.{f.name})
            .HasDatabaseName("IX_{spec.entity}_{f.name.removesuffix('Id')}");''')

        if f.unique_with_company:
            index_configs.append(f'''        builder
            .HasIndex(e => new {{ e.CompanyId, e.{f.name} }})
            .IsUnique()
            .HasDatabaseName("UX_{spec.entity}_CompanyId_{f.name}");''')

        fluent_calls = []
        if f.required:
            fluent_calls.append(".IsRequired()")
        if f.max_length:
            fluent_calls.append(f".HasMaxLength({f.max_length})")
        if f.precision:
            fluent_calls.append(f".HasPrecision({f.precision[0]}, {f.precision[1]})")
        if f.column_type:
            fluent_calls.append(f'.HasColumnType("{f.column_type}")')
        if f.default_value is not None:
            fluent_calls.append(f".HasDefaultValue({cs_literal(f.default_value)})")
        if f.default_value_sql:
            escaped_sql = f.default_value_sql.replace('"', '\\"')
            fluent_calls.append(f'.HasDefaultValueSql("{escaped_sql}")')
        fluent_calls.extend(normalize_fluent_call(call) for call in f.fluent)
        if fluent_calls:
            config = f'''        builder
            .Property(e => e.{f.name})'''
            for i, call in enumerate(fluent_calls):
                terminator = ";" if i == len(fluent_calls) - 1 else ""
                config += f"\n            {call}{terminator}"
            property_configs.append(config)

        if f.relation and f.name.endswith("Id"):
            nullable_marker = "?" if f.type.endswith("?") else ""
            default_value = "" if nullable_marker else " = default!;"
            nav_props.append(f"    public {f.relation}{nullable_marker} {f.relation} {{ get; set; }}{default_value}")
            relation_configs.append(f'''        builder
            .HasOne(e => e.{f.relation})
            .WithMany()
            .HasForeignKey(e => e.{f.name})
            .OnDelete(DeleteBehavior.NoAction);''')

    fields_code = []
    for f in spec.fields:
        attrs = [normalize_attr(attr) for attr in [*f.attributes, *f.model_attributes]]
        fields_code.append(property_with_attributes(
            f.model_type,
            f.name,
            cs_default_property(f),
            attrs,
        ))

    status_code = ""
    status_map = ""
    if spec.status:
        status_code = f'''
    public bool Status {{ get; private set; }} = true;
    public DateTime? StatusChangeDate {{ get; private set; }}

    public void ChangeStatus(bool newStatus, DateTime? statusChangeDate)
    {{
        if (Status == newStatus)
            return;

        Status = newStatus;
        StatusChangeDate = newStatus ? null : statusChangeDate;
    }}
'''
        status_map = f'''
            .ToTable(t => t.HasCheckConstraint(
                "CK_{spec.entity}_StatusChangeDate",
                "([Status] = 0 AND [StatusChangeDate] IS NOT NULL) OR ([Status] = 1 AND [StatusChangeDate] IS NULL)"
            ))'''

    annotation_using = "using System.ComponentModel.DataAnnotations;\n\n" if any(
        field.attributes or field.model_attributes for field in spec.fields
    ) else ""

    return f'''{annotation_using}using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NGErp.Base.Domain.Entities;

namespace NGErp.{spec.module}.Domain.Entities;

public class {spec.entity} :
    {spec.base_class},
    IBaseEntityTypeConfiguration<{spec.entity}>
{{
{chr(10).join(fields_code)}
{chr(10).join(nav_props)}{status_code}
    public void Map(EntityTypeBuilder<{spec.entity}> builder)
    {{
        builder
            .ToTable(nameof({spec.entity}), "{spec.schema}"){status_map};

{chr(10).join(index_configs)}

{chr(10).join(property_configs)}

{chr(10).join(relation_configs)}
    }}
}}
'''


def generate_dto(spec: EntitySpec) -> str:
    dto_props = ["    public Guid Id { get; set; }"]
    create_props = []
    patch_props = []

    for f in spec.fields:
        dto_attrs = field_data_annotations(f, include_required=False) + [
            normalize_attr(attr) for attr in f.dto_attributes
        ]
        dto_props.append(property_with_attributes(
            f.model_type,
            f.name,
            cs_default_property(f),
            dto_attrs,
        ))
        if f.relation and f.name.endswith("Id"):
            dto_props.append(f"    public {f.relation}Dto? {f.relation} {{ get; set; }}")

        create_type = dto_type_for_create(f)
        create_attrs = field_data_annotations(f, include_required=True) + [
            normalize_attr(attr) for attr in f.create_attributes
        ]
        patch_attrs = field_data_annotations(f, include_required=False) + [
            normalize_attr(attr) for attr in f.patch_attributes
        ]
        create_props.append(property_with_attributes(create_type, f.name, "", create_attrs))
        patch_props.append(property_with_attributes(dto_type_for_patch(f), f.name, "", patch_attrs))

    status_code = ""
    change_status_code = ""
    if spec.status:
        dto_props.extend([
            "    public bool Status { get; set; } = true;",
            "    public DateTime? StatusChangeDate { get; set; }",
        ])
        change_status_code = f'''

public class {spec.entity}ChangeStatusDto
{{
    public bool Status {{ get; set; }}
    public DateOnly? Date {{ get; set; }}
}}
'''

    return f'''using System.ComponentModel.DataAnnotations;

namespace NGErp.{spec.module}.Service.DTOs;

public class {spec.entity}Dto
{{
{chr(10).join(dto_props)}
}}

public class Create{spec.entity}Dto
{{
{chr(10).join(create_props)}
}}

public class Patch{spec.entity}Dto
{{
{chr(10).join(patch_props)}
}}{change_status_code}
'''


def generate_repository_contract(spec: EntitySpec) -> str:
    return f'''using NGErp.General.Service.Repository.Contracts;
using NGErp.{spec.module}.Domain.Entities;

namespace NGErp.{spec.module}.Service.Repository.Contracts;

public interface I{spec.entity}Repository : IRepositoryWithCompany<{spec.entity}>
{{ }}
'''


def generate_repository(spec: EntitySpec) -> str:
    return f'''using NGErp.Base.Infrastructure.DataAccess;
using NGErp.General.Infrastructure.DataAccess.Repositories;
using NGErp.{spec.module}.Domain.Entities;
using NGErp.{spec.module}.Service.Repository.Contracts;

namespace NGErp.{spec.module}.Infrastructure.DataAccess.Repositories;

public class {spec.entity}Repository(MainDbContext context) :
    RepositoryWithCompany<{spec.entity}>(context),
    I{spec.entity}Repository
{{ }}
'''


def generate_parameters(spec: EntitySpec) -> str:
    return f'''using NGErp.Base.Service.RequestFeatures;

namespace NGErp.{spec.module}.Service.RequestFeatures;

public class {spec.entity}Parameters : RequestParameters
{{
}}
'''


def generate_service_interface(spec: EntitySpec) -> str:
    status_method = ""
    if spec.status:
        status_method = f'''

    Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        {spec.entity}ChangeStatusDto changeStatusDto,
        CancellationToken ct
    );'''

    return f'''using Microsoft.AspNetCore.JsonPatch;

using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.{spec.module}.Service.DTOs;
using NGErp.{spec.module}.Service.RequestFeatures;

namespace NGErp.{spec.module}.Service.Services;

public interface I{spec.entity}Service
{{
    Task<{spec.entity}Dto> CreateAsync(
        Guid companyId,
        Create{spec.entity}Dto createDto,
        CancellationToken ct
    );

    Task<{spec.entity}Dto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    );

    Task<ListResponseModel<{spec.entity}Dto>> GetFilteredAsync(
        Guid companyId,
        {spec.entity}Parameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    );

    Task<{spec.entity}Dto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<Patch{spec.entity}Dto> patchDocument,
        CancellationToken ct
    );

    Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    );{status_method}
}}
'''


def generate_service(spec: EntitySpec) -> str:
    entity = spec.entity
    camel = spec.entity_camel
    status_method = ""
    if spec.status:
        status_method = f'''

    public async Task ChangeStatusAsync(
        Guid companyId,
        Guid id,
        {entity}ChangeStatusDto changeStatusDto,
        CancellationToken ct)
    {{
        var {camel} = await GetByIdOrThrowAsync(companyId, id, trackChanges: true, ct);
        if (changeStatusDto.Date is null)
            throw new ArgumentException("Date is required.");

        {camel}.ChangeStatus(
            changeStatusDto.Status,
            new DateTime(changeStatusDto.Date.Value, TimeOnly.MinValue)
        );

        _{camel}Repository.Update({camel});
        await _{camel}Repository.SaveChangesAsync(ct);
    }}
'''

    return f'''using AutoMapper;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Localization;

using NGErp.Base.Domain.Exceptions;
using NGErp.Base.Service.DTOs;
using NGErp.Base.Service.ResponseModels;
using NGErp.Base.Service.Services;
using NGErp.{spec.module}.Domain.Entities;
using NGErp.{spec.module}.Service.DTOs;
using NGErp.{spec.module}.Service.Repository.Contracts;
using NGErp.{spec.module}.Service.RequestFeatures;
using NGErp.{spec.module}.Service.Resources;

namespace NGErp.{spec.module}.Service.Services;

public class {entity}Service(
    I{entity}Repository {camel}Repository,
    IMapper mapper,
    IStringLocalizer<HCMResource> localizer,
    IAdvancedFilterBuilder filterBuilder
) : I{entity}Service
{{
    private readonly string _key = "{entity}";

    private readonly IMapper _mapper = mapper;
    private readonly IStringLocalizer _localizer = localizer;
    private readonly IAdvancedFilterBuilder _filterBuilder = filterBuilder;
    private readonly I{entity}Repository _{camel}Repository = {camel}Repository;

    public async Task<{entity}Dto> CreateAsync(
        Guid companyId,
        Create{entity}Dto createDto,
        CancellationToken ct
    )
    {{
        var entity = _mapper.Map<{entity}>(createDto);
        entity.CompanyId = companyId;

        var created = await _{camel}Repository.AddAsync(entity, ct);

        await _{camel}Repository.SaveChangesAsync(ct);
        return _mapper.Map<{entity}Dto>(created);
    }}

    public async Task<{entity}Dto> GetByIdAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {{
        var entity = await GetByIdOrThrowAsync(companyId, id, trackChanges, ct);
        return _mapper.Map<{entity}Dto>(entity);
    }}

    public async Task<ListResponseModel<{entity}Dto>> GetFilteredAsync(
        Guid companyId,
        {entity}Parameters parameters,
        FilterNodeDto? filterNodeDto = null,
        CancellationToken ct = default
    )
    {{
        var advancedFilters = _filterBuilder.Build<{entity}>(filterNodeDto);
        var query = _{camel}Repository.GetFiltered(companyId, advancedFilters);
        var res = await _{camel}Repository.GetResponseListAsync(query, parameters, ct);

        return new ListResponseModel<{entity}Dto>(
            results: _mapper.Map<IReadOnlyList<{entity}Dto>>(res.items),
            totalCount: res.count,
            parameters
        );
    }}

    public virtual async Task<{entity}Dto> PatchAsync(
        Guid companyId,
        Guid id,
        JsonPatchDocument<Patch{entity}Dto> patchDocument,
        CancellationToken ct
    )
    {{
        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: false,
            ct
        );

        var patchDto = _mapper.Map<Patch{entity}Dto>(entity);
        var errors = new List<string>();

        patchDocument.ApplyTo(patchDto, error =>
        {{
            errors.Add($"Path: {{error.Operation.path}}, Error: {{error.ErrorMessage}}");
        }});

        if (errors.Count != 0)
        {{
            throw new InvalidPatchDocumentException(errors);
        }}

        _mapper.Map(patchDto, entity);

        await _{camel}Repository.SaveChangesAsync(ct);
        return _mapper.Map<{entity}Dto>(entity);
    }}

    public async Task DeleteAsync(
        Guid companyId,
        Guid id,
        CancellationToken ct
    )
    {{
        var entity = await GetByIdOrThrowAsync(
            companyId,
            id,
            trackChanges: true,
            ct
        );

        _{camel}Repository.Remove(entity);
        await _{camel}Repository.SaveChangesAsync(ct);
    }}{status_method}

    private async Task<{entity}> GetByIdOrThrowAsync(
        Guid companyId,
        Guid id,
        bool trackChanges = false,
        CancellationToken ct = default
    )
    {{
        var entity = await _{camel}Repository.GetByIdAsync(id, trackChanges, ct);
        return entity ?? throw new NotFoundException(_localizer[_key].Value);
    }}
}}
'''


def generate_controller(spec: EntitySpec) -> str:
    entity = spec.entity
    camel = spec.entity_camel
    status_action = ""
    if spec.status:
        status_action = f'''

    [HttpPatch("{{id:guid}}/status")]
    public async Task<IActionResult> ChangeStatus(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] {entity}ChangeStatusDto changeStatusDto,
        CancellationToken ct
    )
    {{
        await _{camel}Service.ChangeStatusAsync(
            companyId,
            id,
            changeStatusDto,
            ct
        );
        return NoContent();
    }}
'''

    return f'''using Asp.Versioning;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

using NGErp.Base.API.ActionFilters;
using NGErp.Base.Service.DTOs;
using NGErp.{spec.module}.Service.DTOs;
using NGErp.{spec.module}.Service.RequestFeatures;
using NGErp.{spec.module}.Service.Services;

namespace NGErp.{spec.module}.API.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "v1-{spec.module.lower()}")]
[Route("api/v{{version:apiVersion}}/companies/{{companyId:guid}}/{spec.module.lower()}/{spec.route_name}")]
public class {entity}Controller(
    I{entity}Service {camel}Service
) : ControllerBase
{{
    private readonly I{entity}Service _{camel}Service = {camel}Service;

    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<IActionResult> Create(
        [FromRoute] Guid companyId,
        [FromBody] Create{entity}Dto createDto,
        CancellationToken ct
    )
    {{
        var dto = await _{camel}Service.CreateAsync(companyId, createDto, ct);

        return CreatedAtAction(
            nameof(GetById),
            new {{ companyId, id = dto.Id }},
            dto
        );
    }}

    [HttpGet("{{id:guid}}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {{
        var dto = await _{camel}Service.GetByIdAsync(
            companyId,
            id,
            trackChanges: true,
            ct
        );

        return Ok(dto);
    }}

    [HttpPost("list")]
    [SkipModelValidation]
    public async Task<IActionResult> Get(
        [FromRoute] Guid companyId,
        [FromQuery] {entity}Parameters parameters,
        [FromBody] FilterNodeDto? filterNodeDto,
        CancellationToken ct
    )
    {{
        var result = await _{camel}Service.GetFilteredAsync(
            companyId,
            parameters,
            filterNodeDto,
            ct
        );

        return Ok(result);
    }}

    [HttpDelete("{{id:guid}}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        CancellationToken ct
    )
    {{
        await _{camel}Service.DeleteAsync(companyId, id, ct);
        return NoContent();
    }}{status_action}

    [HttpPatch("{{id:guid}}")]
    [Consumes("application/json-patch+json")]
    public async Task<IActionResult> Patch(
        [FromRoute] Guid companyId,
        [FromRoute] Guid id,
        [FromBody] JsonPatchDocument<Patch{entity}Dto> patchDocument,
        CancellationToken ct
    )
    {{
        var dto = await _{camel}Service.PatchAsync(
            companyId,
            id,
            patchDocument,
            ct
        );

        return Ok(dto);
    }}
}}
'''


def generate_validator(spec: EntitySpec) -> str:
    if spec.status:
        return f'''using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.{spec.module}.Service.DTOs;
using NGErp.{spec.module}.Service.Resources;

namespace NGErp.{spec.module}.Service.RequestValidators;

public class {spec.entity}ChangeStatusValidator : AbstractValidator<{spec.entity}ChangeStatusDto>
{{
    private readonly IStringLocalizer<HCMResource> _localizer;

    public {spec.entity}ChangeStatusValidator(IStringLocalizer<HCMResource> localizer)
    {{
        _localizer = localizer;

        RuleFor(x => x.Date)
            .NotEmpty()
            .When(x => x.Status == false)
            .WithMessage(_localizer["StatusChangeDate.IsRequired"].Value);
    }}
}}
'''

    return f'''using FluentValidation;

using Microsoft.Extensions.Localization;

using NGErp.{spec.module}.Service.DTOs;
using NGErp.{spec.module}.Service.Resources;

namespace NGErp.{spec.module}.Service.RequestValidators;

public class Create{spec.entity}Validator : AbstractValidator<Create{spec.entity}Dto>
{{
    private readonly IStringLocalizer<HCMResource> _localizer;

    public Create{spec.entity}Validator(IStringLocalizer<HCMResource> localizer)
    {{
        _localizer = localizer;
    }}
}}
'''


def generate_mapper_patch(spec: EntitySpec) -> str:
    entity = spec.entity
    return f'''            CreateMap<{entity}, {entity}Dto>().ReverseMap();
            CreateMap<Create{entity}Dto, {entity}>();
            CreateMap<{entity}, Patch{entity}Dto>().ReverseMap();'''


# -----------------------------
# File operations and patching
# -----------------------------

def write_file(path: Path, content: str, force: bool) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    if path.exists() and not force:
        print(f"SKIP exists: {path}")
        return
    path.write_text(content, encoding="utf-8")
    print(f"WRITE: {path}")


def insert_before_last_brace(path: Path, text: str, marker: str, force: bool) -> None:
    if not path.exists():
        print(f"WARN missing file for patch: {path}")
        return
    content = path.read_text(encoding="utf-8")
    if marker in content:
        print(f"SKIP already patched: {path}")
        return
    pos = content.rfind("}")
    if pos == -1:
        print(f"WARN could not patch, no closing brace: {path}")
        return
    patched = content[:pos].rstrip() + "\n" + text.rstrip() + "\n" + content[pos:]
    if force:
        path.write_text(patched, encoding="utf-8")
        print(f"PATCH: {path}")
    else:
        print(f"DRY PATCH: {path}")


def patch_mapping_profile(paths: ProjectPaths, spec: EntitySpec, force: bool) -> None:
    candidates = list(paths.service.rglob("*MappingProfile.cs"))
    if not candidates:
        print("WARN no MappingProfile file found")
        return
    path = candidates[0]
    text = "\n" + generate_mapper_patch(spec) + "\n"
    insert_before_last_brace(path, text, f"CreateMap<{spec.entity}, {spec.entity}Dto>", force)


def patch_service_collection(paths: ProjectPaths, spec: EntitySpec, force: bool) -> None:
    candidates = list(paths.service.rglob("ServiceCollectionExtensions.cs")) + list(paths.infrastructure.rglob("ServiceCollectionExtensions.cs"))
    for path in candidates:
        content = path.read_text(encoding="utf-8")
        if "AddScoped" not in content:
            continue
        if f"I{spec.entity}Service" in content or f"I{spec.entity}Repository" in content:
            print(f"SKIP already registered: {path}")
            continue

        lines = []
        if "Service.Services" in content:
            lines.append(f"        services.AddScoped<I{spec.entity}Service, {spec.entity}Service>();")
        if "Repository.Contracts" in content:
            lines.append(f"        services.AddScoped<I{spec.entity}Repository, {spec.entity}Repository>();")

        if not lines:
            continue

        insert_point = content.rfind("return services;")
        if insert_point == -1:
            continue
        patched = content[:insert_point] + "\n" + "\n".join(lines) + "\n\n        " + content[insert_point:]
        if force:
            path.write_text(patched, encoding="utf-8")
            print(f"PATCH DI: {path}")
        else:
            print(f"DRY PATCH DI: {path}")


def scaffold(spec: EntitySpec, root: Path, force: bool, patch: bool) -> None:
    paths = resolve_paths(root, spec.module)

    files = {
        paths.domain / "Entities" / f"{spec.entity}.cs": generate_entity(spec),
        paths.service / "DTOs" / f"{spec.entity}Dto.cs": generate_dto(spec),
        paths.service / "Repository.Contracts" / f"I{spec.entity}Repository.cs": generate_repository_contract(spec),
        paths.infrastructure / "DataAccess" / "Repositories" / f"{spec.entity}Repository.cs": generate_repository(spec),
        paths.service / "RequestFeatures" / f"{spec.entity}Parameters.cs": generate_parameters(spec),
        paths.service / "RequestValidators" / f"{spec.entity}Validator.cs": generate_validator(spec),
        paths.service / "Services" / f"I{spec.entity}Service.cs": generate_service_interface(spec),
        paths.service / "Services" / f"{spec.entity}Service.cs": generate_service(spec),
        paths.api / "Controllers" / f"{spec.entity}Controller.cs": generate_controller(spec),
    }

    for path, content in files.items():
        write_file(path, content, force)

    if patch:
        patch_mapping_profile(paths, spec, force=True)
        patch_service_collection(paths, spec, force=True)


# -----------------------------
# CLI
# -----------------------------

def main() -> int:
    parser = argparse.ArgumentParser(description="Generate NGErp entity boilerplate.")
    parser.add_argument("entity", help="Entity name, e.g. EmployeeEducation")
    parser.add_argument("--fields", help="Comma list: Name:Type,Name2:Type2")
    parser.add_argument("--fields-file", help="JSON spec file")
    parser.add_argument("--root", default=".", help="Solution/project root")
    parser.add_argument("--module", default="HCM", help="Module name, default HCM")
    parser.add_argument("--schema", default="HCM", help="DB schema, default HCM")
    parser.add_argument("--base-class", default="BaseEntityWithCompany")
    parser.add_argument("--route", help="Controller route segment, e.g. employee-educations")
    parser.add_argument("--status", action="store_true", help="Generate Status/ChangeStatus support")
    parser.add_argument("--force", action="store_true", help="Overwrite existing generated files")
    parser.add_argument("--patch", action="store_true", help="Patch MappingProfile and DI registration")
    parser.add_argument(
        "--no-prompt-required",
        action="store_true",
        help="Do not interactively confirm required fields",
    )

    args = parser.parse_args()

    try:
        spec = load_spec(args)
        prompt_required_fields(spec, enabled=not args.no_prompt_required)
        root = find_project_root(Path(args.root))
        scaffold(spec, root, force=args.force, patch=args.patch)
        print("Done.")
        return 0
    except Exception as exc:
        print(f"ERROR: {exc}", file=sys.stderr)
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
