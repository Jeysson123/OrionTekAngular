import { Component, OnInit } from '@angular/core';
import { ClientService } from '../../services/client.service';
import { ClientComponent } from '../client/client.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import ClientDao from '../../dao/ClientDao';

@Component({
  selector: 'Home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  imports: [ClientComponent, CommonModule, FormsModule],
})
export class HomeComponent implements OnInit {
  clients: ClientDao[] = [];
  errorMessage: string | null = null;
  currentPage = 0;
  pageSize = 2;
  totalPages = 1;

  showModal: boolean = false;
  newClientName: string = '';
  newClientEnterprise: string = '';

  notificationMessage: string = '';
  showAlert: boolean = false;

  constructor(private clientService: ClientService) {}

  ngOnInit(): void {
    this.loadClients();
  }

  loadClients(): void {
    this.clientService.getClients().subscribe(
      (data: any) => {
        console.log('Received data:', data);
        this.clients = data.body;
        this.totalPages = Math.ceil(this.clients.length / this.pageSize);
      },
      (error: string | null) => {
        console.error('Error loading clients:', error);
        this.errorMessage = error;
      }
    );
  }

  saveClient(): void {
    if (this.newClientName && this.newClientEnterprise) {
      const newClient = new ClientDao(0, this.newClientName, this.newClientEnterprise);
      this.clientService.saveClient(newClient).subscribe(
        (response) => {
          this.notificationMessage = 'Client saved successfully!';
          this.showAlert = true;
          setTimeout(() => {
            this.showAlert = false;
          }, 2000);
          this.loadClients();
          this.toggleModal();
          this.newClientName = '';
          this.newClientEnterprise = '';
        },
        (error) => {
          console.error('Error saving client:', error);
          this.notificationMessage = 'Error saving client!';
          this.showAlert = true;
          setTimeout(() => {
            this.showAlert = false;
          }, 2000);
        }
      );
    } else {
      this.notificationMessage = 'Please fill in all fields.';
      this.showAlert = true;
      setTimeout(() => {
        this.showAlert = false;
      }, 2000);
    }
  }

  toggleModal(): void {
    this.showModal = !this.showModal;
  }

  // Handle client update event from the child component
  onClientUpdated(updatedClient: ClientDao): void {
    const index = this.clients.findIndex((client) => client.id === updatedClient.id);
    if (index !== -1) {
      this.clients[index] = updatedClient; // Update the client in the list
    }
  }

  // Handle client deletion event from the child component
  onClientDeleted(clientId: number): void {
    this.clients = this.clients.filter(client => client.id !== clientId); // Remove deleted client from the list
  }
}
