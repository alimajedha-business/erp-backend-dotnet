using NGErp.Base.Service.Services;
using NGErp.Warehouse.Service.DTOs;

namespace NGErp.Warehouse.Service.Services;

public static class UnitConversionEquationBuilder
{
    private static readonly HashSet<string> AllowedOperators = ["+", "-", "*", "/"];

    public static string? Build(ItemUnitConversionEquationDto dto, int maxUnitOrder)
    {
        var operands = new[]
        {
            dto.Operand1,
            dto.Operand2,
            dto.Operand3,
            dto.Operand4
        };

        var operators = new[]
        {
            dto.Op1,
            dto.Op2,
            dto.Op3
        };

        if (operands.All(IsEmpty) && operators.All(string.IsNullOrWhiteSpace))
            return null;

        if (!IsEmpty(operands[0]) &&
            operands.Skip(1).All(IsEmpty) &&
            operators.All(string.IsNullOrWhiteSpace))
        {
            ValidateOperand(operands[0], 1, maxUnitOrder, required: true);

            return FormatOperand(operands[0]);
        }

        ValidateOperand(operands[0], 1, maxUnitOrder, required: true);
        ValidateOperand(operands[1], 2, maxUnitOrder, required: true);
        ValidateOperator(operators[0], 1, required: true);

        var equation = $"{FormatOperand(operands[0])} {operators[0]} {FormatOperand(operands[1])}";

        for (var i = 1; i < operators.Length; i++)
        {
            var op = operators[i];
            var operand = operands[i + 1];

            var hasOp = !string.IsNullOrWhiteSpace(op);
            var hasOperand = !IsEmpty(operand);

            if (hasOp != hasOperand)
                throw new ArgumentException($"op{i + 1} and operand{i + 2} must either both be provided or both be empty.");

            if (!hasOp)
                continue;

            ValidateOperator(op, i + 1, required: true);
            ValidateOperand(operand, i + 2, maxUnitOrder, required: true);

            equation = $"({equation}) {op} {FormatOperand(operand)}";
        }

        if (equation.Length > 100)
            throw new ArgumentException("Conversion equation is too long. Max length is 100 characters.");

        return equation;
    }

    public static ItemUnitConversionEquationDto Parse(string equation)
    {
        if (string.IsNullOrWhiteSpace(equation))
        {
            throw new ArgumentException("Conversion equation is required.");
        }

        var parts = FlattenLeftAssociativeExpression(equation.Trim());

        if (parts.Count is not 1 and not 3 and not 5 and not 7)
        {
            throw new ArgumentException("Conversion equation has invalid format.");
        }

        var operands = new object?[4];
        var operators = new string?[3];

        operands[0] = ParseOperand(parts[0]);

        for (var i = 1; i < parts.Count; i += 2)
        {
            var operatorIndex = (i - 1) / 2;
            var operandIndex = operatorIndex + 1;

            operators[operatorIndex] = parts[i];
            operands[operandIndex] = ParseOperand(parts[i + 1]);
        }

        return new ItemUnitConversionEquationDto
        {
            Operand1 = operands[0],
            Operand2 = operands[1],
            Operand3 = operands[2],
            Operand4 = operands[3],
            Op1 = operators[0] ?? string.Empty,
            Op2 = operators[1],
            Op3 = operators[2]
        };
    }

    private static void ValidateOperand(object? value, int index, int maxUnitOrder, bool required)
    {
        if (IsEmpty(value))
        {
            if (required)
                throw new ArgumentException($"operand{index} is required.");

            return;
        }

        var str = value!.ToString()!.Trim();

        if (RegexHelpers.UnitOfMeasurementRefRegex().IsMatch(str))
        {
            var unitOrder = int.Parse(str[2..]);

            if (unitOrder > maxUnitOrder)
                throw new ArgumentException($"operand{index} references {str}, but only @u1 to @u{maxUnitOrder} are allowed.");

            return;
        }

        if (!decimal.TryParse(str, out _))
            throw new ArgumentException($"operand{index} must be a number or unit reference like @u1.");
    }

    private static void ValidateOperator(string? op, int index, bool required)
    {
        if (string.IsNullOrWhiteSpace(op))
        {
            if (required)
                throw new ArgumentException($"op{index} is required.");

            return;
        }

        if (!AllowedOperators.Contains(op.Trim()))
            throw new ArgumentException($"op{index} must be one of: +, -, *, /.");
    }

    private static string FormatOperand(object? value)
    {
        return value!.ToString()!.Trim();
    }

    private static bool IsEmpty(object? value)
    {
        return value is null || string.IsNullOrWhiteSpace(value.ToString());
    }

    private static List<string> FlattenLeftAssociativeExpression(string expression)
    {
        var result = new List<string>();
        Flatten(expression, result);
        return result;
    }

    private static void Flatten(string expression, List<string> result)
    {
        expression = StripOuterParentheses(expression.Trim());

        var split = TrySplitAtTopLevelOperator(expression);

        if (split is null)
        {
            result.Add(expression);
            return;
        }

        Flatten(split.Value.Left, result);
        result.Add(split.Value.Operator);
        result.Add(split.Value.Right.Trim());
    }

    private static (string Left, string Operator, string Right)? TrySplitAtTopLevelOperator(string expression)
    {
        var depth = 0;

        for (var i = expression.Length - 1; i >= 0; i--)
        {
            var ch = expression[i];

            if (ch == ')')
            {
                depth++;
                continue;
            }

            if (ch == '(')
            {
                depth--;
                continue;
            }

            if (depth == 0 && IsOperator(ch))
            {
                return (
                    expression[..i].Trim(),
                    ch.ToString(),
                    expression[(i + 1)..].Trim()
                );
            }
        }

        return null;
    }

    private static string StripOuterParentheses(string expression)
    {
        while (
            expression.Length >= 2 &&
            expression[0] == '(' &&
            expression[^1] == ')' &&
            OuterParenthesesWrapWholeExpression(expression)
        )
        {
            expression = expression[1..^1].Trim();
        }

        return expression;
    }

    private static bool OuterParenthesesWrapWholeExpression(string expression)
    {
        var depth = 0;

        for (var i = 0; i < expression.Length; i++)
        {
            if (expression[i] == '(')
            {
                depth++;
            }
            else if (expression[i] == ')')
            {
                depth--;

                if (depth == 0 && i < expression.Length - 1)
                    return false;
            }

            if (depth < 0)
                return false;
        }

        return depth == 0;
    }

    private static bool IsOperator(char ch)
    {
        return ch is '+' or '-' or '*' or '/';
    }

    private static object? ParseOperand(string operand)
    {
        operand = operand.Trim();

        if (RegexHelpers.UnitOfMeasurementRefRegex().IsMatch(operand))
            return operand;

        if (decimal.TryParse(operand, out var number))
            return number;

        throw new ArgumentException($"Invalid operand '{operand}'.");
    }
}
