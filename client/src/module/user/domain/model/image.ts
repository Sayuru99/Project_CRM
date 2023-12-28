interface ImageI {
  content?: File | null;
}

class ImageModel {
  content?: File | null;

  constructor(data: ImageI) {
    this.content = data.content ?? null;
  }
}

export { ImageModel };
