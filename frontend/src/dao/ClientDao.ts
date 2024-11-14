export default class ClientDao {
    id: number;
    name: string;
    enterprise: string;
  
    constructor(id: number, name: string, enterprise: string) {
      this.id = id;
      this.name = name;
      this.enterprise = enterprise;
    }
  }
  