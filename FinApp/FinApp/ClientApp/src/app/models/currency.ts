export class Currency {
  Id: number;
  Name: string;
  ExchengeRate: number;

  constructor(id: number,
    name: string,
    exchengeRate: number) {
    this.Id = id;
    this.Name = name;
    this.ExchengeRate = exchengeRate;
  }
}
