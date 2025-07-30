import { ContractType, RemoteType } from "./models/offer.model";

export function getContractTypeLabel(contract: ContractType): string {
    switch (contract) {
        case 0: return 'CDI';
        case 1: return 'CDD';
        case 2: return 'Freelance';
        case 3: return 'Stage';
        case 4: return 'Alternance';
        default: return 'Non spécifié';
    }
}

export function getRemoteTypeLabel(remoteType: RemoteType): string {
    switch (remoteType) {
        case 0: return 'Présentiel';
        case 1: return 'Hybride';
        case 2: return 'Remote';
        default: return 'Non spécifié';
    }
}