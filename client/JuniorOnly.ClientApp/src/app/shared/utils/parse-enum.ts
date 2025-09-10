export function parseEnum<T>(value: number | string | null | undefined, enumType: any): T | undefined {
    if (value === null || value === undefined || value === 'no-preference') return undefined;

    if (typeof value === 'number') {
        return enumType[value] !== undefined ? value as T : undefined;
    }
    const num = Number(value);
    if (!isNaN(num) && enumType[num] !== undefined) {
        return num as T;
    }

    return undefined;
}