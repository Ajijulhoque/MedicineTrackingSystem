
import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  public medicines : IMedicine[];
  private baseUrl : string;

  constructor(private  http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  public ngOnInit(): void {
    this.http.get<IMedicine[]>(this.baseUrl + 'Medicines').subscribe(result => {
      this.medicines = result;
    }, error => console.error(error));

    //this.medicines = <IMedicine[]>([
    //  { fullName: "Med1", brand: "Brand" },
    //  { fullName: "Med2", brand: "Brand" },
    //  { fullName: "Med3", brand: "Brand" }
    //]);
  }
}

interface IMedicine {
  fullName: string;
  brand: string;
  price: number;
  quantity: number;
  expiryDate: string;
  notes: string;
}
