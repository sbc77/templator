# templator
Simple templating engine for generating printouts (labels, invoices, etc) in PDF or SVG

.Net Core 2.2

You can turn this:
``` XML
<?xml version="1.0" encoding="UTF-8"?>
<Template Debug="true">
    <PageSettings Format="A5" Orientation="Portrait" Margin="15" />
    <PageHeader>
        <Image Src="logo.png" Width="90" Height="30" />
    </PageHeader>
    <ReportBody>
        <Field DataField="CustomerName" />
        <Field DataField="CustomerStreet" />
        <Field DataField="CustomerZip" />
        <Field DataField="CustomerCity" />
        <Line />
        <Field DataField="SsccNo" />
        <Field DataField="ArticleId" />
        <Field DataField="ArticleEan" />
        <Field DataField="ArticleDescription" />
        <Field DataField="Quantity" />
        <Field DataField="CustomerReference" />
        <Line />
        <Barcode Type="GS1-128" DataField="Barcode1" />
        <Barcode Type="GS1-128" DataField="Barcode2" />
    </ReportBody>
</Template>
```
into this:
![Example label](example.png "Label")
using this code:
```C#
var data = new Gs1Data
      {
          CustomerName = "Awesome Company Ltd",
          CustomerStreet = "Industriestrasse 666",
          CustomerZip = "CH 2555",
          CustomerCity = "Brügg",
          SsccNo = "376113650000131748",
          ArticleDescription = "Ordner, 7 cm, gelb",
          ArticleId = "07610811240002",
          ArticleEan = "07610811240002",
          CustomerReference = "4591354435",
          Quantity = 40,
          Barcode1 = "(02)07611365331178(37)112(400)20216916",
          Barcode2 = "(00)376113650002691578"
      };

      var cfg = new PdfConfig
      {
          OutFile = "result.pdf",
          FontPaths = new string[] { "/Library/Fonts" } // this is configured for macOS, on windows/linux you have to change it
      };

      Templator.Create("label.xml")
              .UsePdfRenderer(cfg)
              .Render(data);
```
