# Virtual Reality und 3D Interaktionen im Sommersemester 2024
Die Projekte für das Modul "VR und 3D Interaktion" im Sommersemester 2024

Wird für jedes Projekt erweitert.

## Projekt 1: Ames Raum
Im ersten Projekt sollte die optische Täuschung eines Ames Raum in Unity nachgebaut werden. In Aufgabenteil a) sollte zunächst der Raum mit einer statischen Kamera gebaut werden, in Teil b) sollte dann das Ansehen des Raums mit einer VR-Brille ermöglicht werden.

In Teil a) haben wir über die Aufgabenstellung hinaus den Raum mit Fenstern, einem Bild und einer Tür dekoriert, um die Illusion zu verstärken. Außerdem haben wir ein laufendes Skelett eingebaut, dass von links nach rechts läuft und dabei wächst und schrumpft. Selbstverständlich ist das nur die optische Täuschung, die Skalierung vom Skelett wird nicht verändert.

In Teil b) haben wir zusätzlich eine Mauer mit Guckloch gebaut, sodass man mehr oder weniger gezwungen wird aus einem Blickwinkel in den Raum zu schauen, aus dem die Illusion stark ist. Rechts vom Spieler befindet sich eine Bombe, die aufgenommen und gegen die Wand geschmissen werden kann. Die Bombe bringt die Mauer zum Einsturz, wodurch man den Raum nun komplett betrachten kann. Außerdem kann auch hier eine Kamerafahrt gestartet werden, sodass auch in VR die Illusion vollständig verschwinden kann.


### How To?
In Teil a) kann die Kamerafahrt mit Leertaste gestartet werden. Die Kamera bewegt sich auf eine neue Position und wartet dort 5 Sekunden. So geht sie auf mehrere Positionen, bis die Ausgangsposition wieder erreicht ist.

In Teil b) kann die Kamerafahrt ähnlich genutzt werden. Dafür muss zunächst die Mauer zerstört werden. Die Bombe kann entweder mit der Seitentaste des Controllers oder einer Pinch-Geste mit der Hand gegriffen werden. Dabei haben wir uns für eine Variante entschieden, bei der die Bombe auch auf Distanz gegriffen werden kann, um das Navigieren in VR zu vereinfachen. Wurde die Mauer zum Einsturz gebracht, so kann die Position entweder mit der "A"-Taste des rechten Controllers oder einer Thumbs Up Geste der rechten Hand verändert werden. Die Positionen sind diegleichen wie in Teil a), jedoch wurde sich hierbei für Teleportation entschieden, um Motion Sickness vorzubeugen.

### Bildergalerie

<img src="https://github.com/RothRobe/VR-SS24/assets/82387986/dc15cd65-0caf-4d4f-9308-38f6775a85ed" width="500">
<img src="https://github.com/RothRobe/VR-SS24/assets/82387986/d7c9aaeb-0ff2-400c-8a90-38ef4318fa34" width="500">
Das Skelett sieht optisch auf der linken Seite des Raums größer aus, als auf der rechten Seite des Raums.

<img src="https://github.com/RothRobe/VR-SS24/assets/82387986/ee203950-678c-44b7-b6c6-49ab38e73392" width="500">
<img src="https://github.com/RothRobe/VR-SS24/assets/82387986/d2d8441c-8875-47ad-aa92-d36de6c58ed1" width="500">
In VR kann entweder mit den Controllern oder Handgesten interagiert werden.


### Verwendete Assets
[Meta XR All-in-One SDK](https://assetstore.unity.com/packages/tools/integration/meta-xr-all-in-one-sdk-269657)

[Unity Particle Pack](https://assetstore.unity.com/packages/vfx/particles/particle-pack-127325)

[Skeleton Animations FREE](https://assetstore.unity.com/packages/3d/skeleton-animations-free-217504)

[Soundeffekte von pixabay](https://pixabay.com/)
