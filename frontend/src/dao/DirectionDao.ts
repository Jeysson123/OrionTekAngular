class DirectionDao {
    id: number;
    clientId: number;
    address: string;
  
    constructor(id: number, clientId: number, address: string) {
      this.id = id;
      this.clientId = clientId;
      this.address = address;
    }
  }
  
  export default DirectionDao;
  