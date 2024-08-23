# 15Puzzle Game
15 puzzle oyununda gridin her hücresinde farklı bir sayı yazar ve boş bir hücre bulundurur. Oyunun amacı sayıları doğru sıralamaya getirmektir. Hücreler yatay ve dikey eksende hareket ettirilebilir. 

## Gridler
Her seviyede farklı gridler bulunmaktadır ve bu oyunun zorluğunu etkileyen bir unsurdur. Her seviye daha önceden rastgele bir şekilde oluşturulmuş gridleri barındırmaktadır. Gridler editor üzerinden oluşturulurken tüm sayılar rastgele konumlara yerleştirilir ve yine rastgele belirlenmiş bir konum boş bırakılır. 

<img src= "https://github.com/user-attachments/assets/34f1be87-fca3-43d9-8fa7-c41c583f75a6" width = "200px"> 
<img src= "https://github.com/user-attachments/assets/557dff82-b06c-4047-a5ff-ec744a531001" width = "202px"> 


## Oyun Türleri
  1. ### Classic
     Bu oyun türünde sayılar her satırda soldan sağa şekilde dizilmelidir.
<img src= "https://github.com/user-attachments/assets/e5990b36-fef6-4155-817d-b6eb0e16118c" width = "200px">

  3. ### Snake
     Bu oyun türünde sayılar her satırda birbirinin ters yönünde dizilmelidir. Örneğin ilk satır soldan sağa, ikinci satır sağdan sola şeklinde olmalıdır.
<img src= "https://github.com/user-attachments/assets/3313d72d-b79f-4363-90e9-fe49af000943" width = "200px">

  4. ### Spiral
     Bu oyun türünde sayılar sol üst köşeden başlayarak satırın en sonuna kadar ilerler. Sonrasında yukarıdan aşağı ilerler, ardından sola doğru ilerler. Ve bu işlem merkeze gelene kadar devam eder.

<img src= "https://github.com/user-attachments/assets/a293e36d-a152-418b-becb-6ea9a22a41f7" width = "200px"> 

## Oyun Modları
  1. ### Normal
     Bu modda ekstra bir şey olmadan oyuncu oyunu oynamaya devam eder.
  2. ### Hard
     Bu oyun modunda oyuncu sayıları yalnızca oyun başlamadan önce ve UI'da bulunan "peek" butonuna tıklarken görebilir. Oyunu sayıları görmeden bitirmelidir.

<img src= "https://github.com/user-attachments/assets/1627ce7b-0276-4b82-b665-1d76712dfc64" width = "200px">
<img src= "https://github.com/user-attachments/assets/b4015dc0-8c46-4b26-ac31-2d7813fb0565" width="200px">


## Kayıtlar
  Her level tamamlandığında level içerisindeki bilgiler bir json dosyasına kaydedilir. Ve bu bilgiler ayarlar sekmesinde listelenir.
  
<img src= "https://github.com/user-attachments/assets/a7cb5274-b23f-4799-bb1c-8cea8653491d" width="200px">
<img src= "https://github.com/user-attachments/assets/ea604e8d-318a-42cf-8ad7-e2e720f18e94" width="400px">


## Oyun İçi UI
  Oyun içerisinde detaylı bilgiler bulunduran paneller vardır. Bu bilgiler: level türü, modu, level sayısı, geçen süre, hareket sayısıdır. Bunun dışında oyunu durdurma, yeniden başlatma, ayarlar ve sadece zor modda gözüken sayıları görüntüleme butonları bulunmaktadır.
 
<img src= "https://github.com/user-attachments/assets/0b767eff-a9af-4cfa-9333-d7e3bff98c3d" width="200px">

## Ayarlar UI
  Ayarlarda oyunun müziğini ve ses efektlerini açıp / kapatmak için butonlar bulunur. Bunun dışında her level bitiminde kayıt edilen bilgiler de listelenmektedir.
 
<img src= "https://github.com/user-attachments/assets/c5289edd-8241-44b7-9cd6-c8b9867e15f3" width="200px">

## Başlangıç UI 
  Oyuna giren oyuncunun karşılaşacağı ilk ekrandır. Oyuncunun kaldığı leveldan devam etmesi için ve ayarları açabilmesi için butonlar bulunur.
 
<img src= "https://github.com/user-attachments/assets/8951e710-0d37-4c0e-a267-737a2af60da3" width="200px">

## Oyun Sonu UI
  Seviye tamamlandıktan sonra gelen ekrandır. Bir sonraki levela geçmek için bir buton bulundurur. 

<img src= "https://github.com/user-attachments/assets/a84da25d-a035-427e-95d7-450cc4369e8d" width="200px"> 
