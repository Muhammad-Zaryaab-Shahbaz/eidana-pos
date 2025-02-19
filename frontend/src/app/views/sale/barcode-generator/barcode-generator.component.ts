import { Component } from '@angular/core';
import jsPDF from 'jspdf';
import * as JsBarcode from 'jsbarcode';

@Component({
  selector: 'app-barcode-generator',
  templateUrl: './barcode-generator.component.html',
  styleUrls: ['./barcode-generator.component.css']
})
export class BarcodeGeneratorComponent {
  quantity: number = 1;
  price: number = 0;
  barcodes: string[] = [];
  isProcessing: boolean = false;

  generateBarcodes() {
    this.barcodes = [];
    for (let i = 0; i < this.quantity; i++) {
      let barcodeNumber = this.generateRandomBarcode();
      this.barcodes.push(barcodeNumber);
      setTimeout(() => this.renderBarcode(barcodeNumber, i), 100);
    }
  }

  generateRandomBarcode(): string {
    return Math.floor(100000000 + Math.random() * 900000000).toString();
  }

  renderBarcode(barcode: string, index: number) {
    JsBarcode(`#barcode-${index}`, barcode, {
      format: "CODE128",
      displayValue: true,
      width: 2,
      height: 40,
      fontSize: 14,
      margin: 0
    });
  }

  downloadBarcodesAsPDF() {
    if (this.barcodes.length === 0) return;
    this.isProcessing = true;
  
    setTimeout(() => {
      const doc = new jsPDF({
        orientation: "portrait",
        unit: "mm",
        format: [38, 28],
        hotfixes: ["px_scaling"],
        precision: 1000 // Higher precision for better alignment
      });
  
      this.barcodes.forEach((barcode, index) => {
        if (index > 0) doc.addPage([38, 28], "portrait");
        
        const canvas = document.createElement("canvas");
        JsBarcode(canvas, barcode, {
          format: "CODE128",
          displayValue: true,
          width: 1.8, // Slightly reduced width for better fit
          height: 40,
          fontSize: 14,
          margin: 0,
          textMargin: 0
        });
  
        // Add company name
        doc.setFontSize(14);
        doc.setFont("helvetica", "bold");
        doc.text("Eidanas", 19, 4, { align: "center", baseline: "top" });
  
        // Add barcode image
        const imgData = canvas.toDataURL("image/png");
        const imgProps = doc.getImageProperties(imgData);
        const imgWidth = 34; // Reduced width to prevent cutting
        const imgHeight = (imgProps.height * imgWidth) / imgProps.width;
        const xPos = (38 - imgWidth) / 2; // Center calculation
        doc.addImage(imgData, 'PNG', xPos, 7, imgWidth, imgHeight);
  
        // Add price
        doc.setFontSize(12);
        doc.setFont("helvetica", "normal");
        doc.text(`Price: PKR ${this.price}`, 19, 25, { 
          align: "center",
          baseline: "bottom"
        });
      });
  
      doc.save("barcodes.pdf");
      this.isProcessing = false;
    }, 500);
  }
  
  printBarcodes() {
    const printContent = document.getElementById("barcode-print")?.innerHTML;
    if (!printContent) return;
  
    const printWindow = window.open("", "_blank");
    if (printWindow) {
      printWindow.document.open();
      printWindow.document.write(`
        <html>
          <head>
            <title>Print Barcodes</title>
            <style>
              ${document.querySelector('style')?.innerHTML}
              body { 
                margin: 0 !important; 
                padding: 0 !important;
                -webkit-print-color-adjust: exact !important;
                width: 38mm !important;
              }
              .barcode-item { 
                width: 38mm !important; 
                height: 28mm !important;
                page-break-after: always !important;
                position: relative !important;
              }
              .company-name,
              .barcode-price {
                text-align: center !important;
                width: 100% !important;
                padding: 0 2mm !important;
                position: absolute !important;
              }
              .company-name {
                top: 1mm !important;
              }
              .barcode-price {
                bottom: 1mm !important;
              }
              .barcode {
                position: absolute !important;
                top: 50% !important;
                transform: translateY(-50%) !important;
                width: 90% !important;
                left: 5% !important;
              }
            </style>
          </head>
          <body onload="window.print(); window.close();">
            ${printContent}
          </body>
        </html>
      `);
      printWindow.document.close();
    }
  }
}