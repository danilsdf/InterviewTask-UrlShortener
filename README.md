# Tiny URL
Develop a web application that creates a tiny url for an uploaded image.

## Performance:
Limit upload size to 10MB
Accept multiple MIME types including HEIC/HEIF, WEBP, PNG, JPEG, SVG, PDF & GIF (BONUS - Accept a proprietary format like AI, FIG, SKETCH)

Run a server side binary or use a Cloud Service API to optimize the image with minimal reduction in quality, maximal reduction in size

Store the image as a data blob or a path to a file
Host the image path on a CDN like S3 or store its data in local database like MySQL or Mongo,
  or by using a remote database service like Firebase
Use unreserved alphanumeric characters for the shortened URI (e.g., app.io/A18fhsH)
The endpoint for the generated URI should serve HTML that web scrapers can easily digest,
  e.g. write tags for the Open Graph Protocol (http://ogp.me/) and Search Engines like Google, DuckDuckGo
Host on free services like Netlify, Serverless.com, Heroku, or AWS

## Appearance:
Use a simple, attractive design and logo: https://logomaster.ai
Use a Google Font: https://fonts.google.com
Use a popular CSS framework: BlueprintJS, Bootstrap, Material, Evergreen, Bulma
Code:

Code should be commented and easy to read
Store on Github or Gitlab
Be prepared to explain your logic
Avoid copy / paste solutions but use yarn or npm libraries if needed
Time Limit: 8 hrs
