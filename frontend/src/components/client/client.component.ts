import { Component, Input, Output, EventEmitter, ChangeDetectorRef, OnInit } from '@angular/core';
import { ClientService } from '../../services/client.service';
import ClientDao from '../../dao/ClientDao';
import DirectionDao from '../../dao/DirectionDao';
import { DirectionService } from '../../services/direction.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'Client',
  standalone: true,
  templateUrl: './client.component.html',
  styleUrls: ['./client.component.css'],
  imports: [CommonModule, FormsModule],
})
export class ClientComponent implements OnInit {
  @Input() client: ClientDao | any;
  @Output() clientUpdated = new EventEmitter<ClientDao>();
  @Output() clientDeleted = new EventEmitter<number>();

  currentClient: ClientDao = new ClientDao(0, '', '');
  showModal: boolean = false;
  showAlert: boolean = false;
  notificationMessage: string = '';
  clientDirections: DirectionDao[] = [];
  showDirectionsModal: boolean = false;
  showCreateDirectionModal: boolean = false;
  showEditDirectionModal: boolean = false; // Flag to show edit direction modal
  newDirection: DirectionDao = new DirectionDao(0,0,'');
  editDirectionData: DirectionDao = new DirectionDao(0, 0, ''); // Holds direction being edited

  constructor(
    private clientService: ClientService,
    private directionService: DirectionService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    if (this.client && this.client.id) {
      this.getDirectionsForClient(this.client.id);
    }
  }

  toggleModal(): void {
    this.showModal = !this.showModal;
  }

  toggleDirectionsModal(): void {
    this.showDirectionsModal = !this.showDirectionsModal;
  }

  toggleCreateDirectionModal(): void {
    this.showCreateDirectionModal = !this.showCreateDirectionModal;
  }

  toggleEditDirectionModal(): void {
    this.showEditDirectionModal = !this.showEditDirectionModal;
  }

  editClient(client: ClientDao): void {
    this.currentClient = { ...client };
    this.showModal = true;
  }

  updateClient(): void {
    this.clientService.updateClient(this.currentClient).subscribe(
      (response) => {
        this.feedback('Client updated successfully!');
        this.toggleModal();
        this.clientUpdated.emit(this.currentClient);
      },
      (error) => {
        console.error('Error updating client:', error);
        this.feedback('Error updating client');
      }
    );
  }

  deleteClient(clientId: number): void {
    if (confirm('Are you sure you want to delete this client?')) {
      this.clientService.deleteClient(clientId).subscribe(
        (response) => {
          this.feedback('Client deleted successfully!');
          this.clientDeleted.emit(clientId);
        },
        (error) => {
          console.error('Error deleting client:', error);
          this.feedback('Error deleting client');
        }
      );
    }
  }

  getDirectionsForClient(clientId: number): void {
    this.directionService.getDirectionsByClient(clientId).subscribe(
      (directions: any) => {
        this.clientDirections = directions.body;
      },
      (error) => {
        console.error('Error fetching directions:', error);
      }
    );
  }

  createNewDirection(): void {
    this.toggleCreateDirectionModal();
  }

  createDirection(): void {
    this.newDirection.clientId = this.client.id;
    this.directionService.saveDirection(this.newDirection).subscribe(
      (response) => {
        this.feedback('New direction added successfully!');
        this.clientDirections.push(response);
        this.toggleCreateDirectionModal();
      },
      (error) => {
        console.error('Error creating direction:', error);
        this.feedback('Error creating direction');
      }
    );
  }

  editDirection(direction: DirectionDao): void {
    this.editDirectionData = { ...direction };
    this.toggleEditDirectionModal();
  }

  updateDirection(): void {
    this.directionService.updateDirection(this.editDirectionData).subscribe(
      (response) => {
        this.feedback('Direction updated successfully!');
        const index = this.clientDirections.findIndex(dir => dir.id === this.editDirectionData.id);
        if (index > -1) {
          this.clientDirections[index] = { ...this.editDirectionData };
        }
        this.toggleEditDirectionModal();
      },
      (error) => {
        console.error('Error updating direction:', error);
        this.feedback('Error updating direction');
      }
    );
  }

  removeDirection(directionId: number): void {
    if (confirm('Are you sure you want to remove this direction?')) {
      this.directionService.deleteDirection(directionId).subscribe(
        (response) => {
          this.feedback('Direction removed successfully!');
          this.clientDirections = this.clientDirections.filter(direction => direction.id !== directionId);
        },
        (error) => {
          console.error('Error removing direction:', error);
          this.feedback('Error removing direction');
        }
      );
    }
  }

  private feedback(message: string): void {
    this.showAlert = true;
    this.notificationMessage = message;
    this.cdr.detectChanges();
    setTimeout(() => {
      this.showAlert = false;
      this.cdr.detectChanges();
    }, 2000);
  }
}
