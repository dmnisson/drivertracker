export class Leg {
    legID: number;
    driverID: number;
    
    startAddress: string;
    pickupRequestTime: string;
    startTime: string;
    destinationAddress: string;
    arrivalTime: string;

    distance: number;
    fare: number; // per-passenger fare
    numOfPassengersAboard: number;
    numOfPassengersPickedUp: number;
    fuelCost: number; // fuel cost per mile

    getTotalFuelCost(): number {
        return this.fuelCost * this.distance;
    }
}