export class CompanyStatistics {
    numOfDrivers: number;
    pickups: number;
    milesDriven: number;
    averagePickupDelay: number;
    totalFares: number;
    totalCosts: number;
    netProfit: number;
}

export class DriverStatistics {
    driverID: number;
    pickups: number; // total pickups
    milesDriven: number; // total miles driven
    averagePickupDelay: number; // average pickup delay in minutes
    totalFares: number;
    totalCosts: number;
}