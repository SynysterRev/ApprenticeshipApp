export function parseEnum<T>(value: number | string | null | undefined, enumType: any): T | undefined {
    if (value === null || value === undefined || value === 'no-preference') return undefined;

    // Si c'est déjà un nombre, vérifier qu'il existe dans l'enum
    if (typeof value === 'number') {
        return enumType[value] !== undefined ? value as T : undefined;
    }

    // Si c'est une string, essayer de la convertir en nombre
    const num = Number(value);
    if (!isNaN(num) && enumType[num] !== undefined) {
        return num as T;
    }

    return undefined;
}