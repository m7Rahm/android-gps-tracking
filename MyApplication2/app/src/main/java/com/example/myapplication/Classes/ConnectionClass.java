package com.example.myapplication.Classes;

import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;

import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;


public class ConnectionClass {
    private String methodName;
    private ArrayList<CarInfoClass> carInfoClasses;
   public ConnectionClass(String methodName) {
       this.methodName = methodName;
   }
   public ArrayList<CarInfoClass> getInitialCoordinates(int rowNum,int branchId) {
       boolean isUrlMalformed = false;
       URL url = null;
       try {
           url = new URL("http://10.1.11.134/myApp/Webservice1.asmx?op=" + methodName);
       } catch (MalformedURLException e) {
           isUrlMalformed = true;
           e.printStackTrace();

       }
       if (!isUrlMalformed) {
           try {
               HttpURLConnection httpURLConnection = (HttpURLConnection) url.openConnection();
               httpURLConnection.setConnectTimeout(5000);
               httpURLConnection.setDoOutput(true);
               httpURLConnection.setDoInput(true);
               httpURLConnection.setRequestMethod("POST");
               httpURLConnection.setRequestProperty("Content-Type", "text/xml; charset=utf-8");
               httpURLConnection.setRequestProperty("SOAPAction", "http://tempuri.org/GetInitialCoordinates");
               String sendData = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                       "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\n" +
                       "  <soap:Body>\n" +
                       "    <GetInitialCoordinates xmlns=\"http://tempuri.org/\">\n" +
                       "      <rownum>" + rowNum + "</rownum>\n" +
                       "      <branchId>" + branchId + "</branchId>\n" +
                       "    </GetInitialCoordinates>\n" +
                       "  </soap:Body>\n" +
                       "</soap:Envelope>";
               httpURLConnection.setRequestProperty("Content-Length", String.valueOf(sendData.length()));
               OutputStream outputStream = httpURLConnection.getOutputStream();
               outputStream.write(sendData.getBytes());
               InputStream bufferedInputStream = httpURLConnection.getInputStream();
               DocumentBuilderFactory dbFactory = DocumentBuilderFactory.newInstance();
               try {
                   DocumentBuilder dBuilder = dbFactory.newDocumentBuilder();
                   Document doc = dBuilder.parse(bufferedInputStream);
                   doc.getDocumentElement().normalize();
                   NodeList nodes = doc.getElementsByTagName("InitialCoordinatesTable");
                   carInfoClasses = new ArrayList<>();
                   for (int i = 0; i < nodes.getLength(); i++) {
                       CarInfoClass carInfoClass = new CarInfoClass();
                       Node currentNode = nodes.item(i);
                       Element eElement = (Element) currentNode;
                       carInfoClass.setLat(Double.valueOf(eElement.getElementsByTagName("lat").item(0).getTextContent()));
                       carInfoClass.setLng(Double.valueOf(eElement.getElementsByTagName("lng").item(0).getTextContent()));
                       carInfoClass.setBranchId(Integer.valueOf(eElement.getElementsByTagName("branch_id").item(0).getTextContent()));
                       carInfoClass.setCarModel(eElement.getElementsByTagName("car_model").item(0).getTextContent());
                       carInfoClass.setLastInfoTime(eElement.getElementsByTagName("insert_date").item(0).getTextContent());
                       carInfoClass.setSpeed(Float.valueOf(eElement.getElementsByTagName("speed2").item(0).getTextContent()));
                       carInfoClass.setCarPlate(eElement.getElementsByTagName("object_num").item(0).getTextContent());
                       carInfoClass.setObjectId(Integer.valueOf(eElement.getElementsByTagName("object_id").item(0).getTextContent()));
                       carInfoClasses.add(carInfoClass);
                   }
               } catch (Exception e) {
                   e.printStackTrace();
               }
           } catch (IOException e) {
               e.printStackTrace();
           }
       }
       return carInfoClasses;
   }
}
