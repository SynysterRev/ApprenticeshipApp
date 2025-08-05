export enum ContractType {
    CDI,
    CDD,
    Freelance,
    Stage,
    Alternance
}

export enum SalaryPeriod {
    Year,
    Month,
    Day
}

export enum RemoteType {
    NoRemote,
    PartialRemote,
    FullRemote
}

export interface BaseOffer {
    title: string;
    description: string;
    location: string;
    contractType: ContractType;
    experienceRequired: number;
    salaryMin: number;
    salaryMax: number;
    salaryPeriod: SalaryPeriod;
    remoteType: RemoteType;
    companyId: string;
    jobSectorId: string;
}

export interface Offer extends BaseOffer {
    id: string;
    companyName: string;
    companyLogo: string;
    publishedAt: Date;
    updatedAt: Date;
}

export interface OfferCreate extends BaseOffer {

}

export interface OfferUpdate {
    title?: string;
    description?: string;
    location?: string;
    contractType?: ContractType;
    experienceRequired?: number;
    salaryMin?: number;
    salaryMax?: number;
    salaryPeriod?: SalaryPeriod;
    remoteType?: RemoteType;
    companyId?: string;
    jobSectorId?: string;
}

export interface SearchCriteria {
    minSalary?: number;
    maxSalary?: number;
    remoteType?: RemoteType;
    location?: string;
    contractType?: ContractType;
    searchTerm? : string;
    pageNumber?: number;
}

